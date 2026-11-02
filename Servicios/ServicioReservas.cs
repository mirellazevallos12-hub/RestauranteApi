using RestauranteApi.Dtos;
using RestauranteApi.Excepciones;
using RestauranteApi.Modelos;
using RestauranteApi.Repositorios;

namespace RestauranteApi.Servicios;

// Aquí están las reglas del restaurante para las reservas:
//  1. La fecha debe ser futura.
//  2. El cliente debe estar registrado.
//  3. La mesa debe existir y tener capacidad para las personas.
//  4. Una mesa no puede tener dos reservas con menos de 2 horas de diferencia.
public class ServicioReservas : IServicioReservas
{
    private const int MinutosEntreReservas = 120;

    private readonly IRepositorioReservas reservas;
    private readonly IRepositorioClientes clientes;
    private readonly IRepositorioMesas mesas;

    public ServicioReservas(IRepositorioReservas reservas, IRepositorioClientes clientes, IRepositorioMesas mesas)
    {
        this.reservas = reservas;
        this.clientes = clientes;
        this.mesas = mesas;
    }

    public List<Reserva> ObtenerTodas()
    {
        return reservas.ObtenerTodas();
    }

    public Reserva ObtenerPorId(int id)
    {
        Reserva? reserva = reservas.ObtenerPorId(id);
        if (reserva == null)
        {
            throw new RecursoNoEncontradoException("No se encontró la reserva con id " + id + ".");
        }
        return reserva;
    }

    public Reserva Crear(CrearReservaRequest peticion)
    {
        DateTime fechaHora = peticion.FechaHora!.Value;
        int numeroMesa = peticion.NumeroMesa!.Value;
        int personas = peticion.CantidadPersonas!.Value;

        ValidarFechaFutura(fechaHora);

        if (clientes.ObtenerPorDni(peticion.ClienteDni) == null)
        {
            throw new RecursoNoEncontradoException("El cliente debe registrarse antes de reservar.");
        }

        Mesa mesa = BuscarMesa(numeroMesa);
        ValidarCapacidad(mesa, personas);
        ValidarMesaLibre(numeroMesa, fechaHora, 0);

        Reserva reserva = new Reserva();
        reserva.ClienteDni = peticion.ClienteDni;
        reserva.NumeroMesa = numeroMesa;
        reserva.FechaHora = fechaHora;
        reserva.CantidadPersonas = personas;
        reserva.Estado = "Confirmada";

        return reservas.Agregar(reserva);
    }

    public Reserva Actualizar(int id, ActualizarReservaRequest peticion)
    {
        Reserva reserva = ObtenerPorId(id);

        if (reserva.Estado != "Confirmada")
        {
            throw new ReglaNegocioException("Solo se puede modificar una reserva confirmada. Estado actual: " + reserva.Estado + ".");
        }

        DateTime fechaHora = peticion.FechaHora!.Value;
        int personas = peticion.CantidadPersonas!.Value;

        ValidarFechaFutura(fechaHora);
        ValidarCapacidad(BuscarMesa(reserva.NumeroMesa), personas);
        ValidarMesaLibre(reserva.NumeroMesa, fechaHora, reserva.Id);

        reserva.FechaHora = fechaHora;
        reserva.CantidadPersonas = personas;
        return reserva;
    }

    // Cancelar: la reserva no se borra, solo cambia su estado a "Cancelada".
    public void Cancelar(int id)
    {
        Reserva reserva = ObtenerPorId(id);

        if (reserva.Estado != "Confirmada")
        {
            throw new ReglaNegocioException("Solo se puede cancelar una reserva confirmada. Estado actual: " + reserva.Estado + ".");
        }

        reserva.Estado = "Cancelada";
    }

    // Finalizar: se usa cuando el cliente ya fue atendido.
    public Reserva Finalizar(int id)
    {
        Reserva reserva = ObtenerPorId(id);

        if (reserva.Estado != "Confirmada")
        {
            throw new ReglaNegocioException("Solo se puede finalizar una reserva confirmada. Estado actual: " + reserva.Estado + ".");
        }

        reserva.Estado = "Finalizada";
        return reserva;
    }

    private Mesa BuscarMesa(int numeroMesa)
    {
        Mesa? mesa = mesas.ObtenerPorNumero(numeroMesa);
        if (mesa == null)
        {
            throw new RecursoNoEncontradoException("La mesa número " + numeroMesa + " no existe.");
        }
        return mesa;
    }

    private void ValidarFechaFutura(DateTime fechaHora)
    {
        if (fechaHora <= DateTime.Now)
        {
            throw new ReglaNegocioException("La fecha y hora de la reserva deben ser futuras.");
        }
    }

    private void ValidarCapacidad(Mesa mesa, int personas)
    {
        if (personas > mesa.Capacidad)
        {
            throw new ReglaNegocioException("La mesa " + mesa.Numero + " tiene capacidad para " + mesa.Capacidad + " personas y se pidieron " + personas + ".");
        }
    }

    // idReservaActual sirve para ignorar la propia reserva cuando se está actualizando.
    private void ValidarMesaLibre(int numeroMesa, DateTime fechaHora, int idReservaActual)
    {
        foreach (Reserva otra in reservas.ObtenerTodas())
        {
            bool esOtraReserva = otra.Id != idReservaActual;
            bool mismaMesa = otra.NumeroMesa == numeroMesa;
            bool estaConfirmada = otra.Estado == "Confirmada";
            double minutosDeDiferencia = Math.Abs((otra.FechaHora - fechaHora).TotalMinutes);

            if (esOtraReserva && mismaMesa && estaConfirmada && minutosDeDiferencia < MinutosEntreReservas)
            {
                throw new ConflictoException("La mesa " + numeroMesa + " ya tiene una reserva con menos de 2 horas de diferencia.");
            }
        }
    }
}
