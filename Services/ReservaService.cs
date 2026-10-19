using RestauranteApi.DTOs;
using RestauranteApi.Exceptions;
using RestauranteApi.Models;
using RestauranteApi.Repositories;

namespace RestauranteApi.Services;

public class ReservaService(
    IReservaRepository reservaRepositorio,
    IMesaRepository mesaRepositorio,
    IClienteRepository clienteRepositorio) : IReservaService
{
    private const int MinutosMinimosEntreReservas = 120;

    public IEnumerable<ReservaResponse> ListarTodas() =>
        reservaRepositorio.ListarTodas().Select(Mapear);

    public ReservaResponse ObtenerPorId(int id) =>
        Mapear(ObtenerReservaExistente(id));

    public ReservaResponse Crear(CrearReservaRequest solicitud)
    {
        lock (BloqueoReservas.Candado)
        {
        var fechaHora = solicitud.FechaHora!.Value;
        var numeroMesa = solicitud.NumeroMesa!.Value;
        var cantidadPersonas = solicitud.CantidadPersonas!.Value;

        ValidarFechaFutura(fechaHora);
        if (clienteRepositorio.BuscarPorDni(solicitud.ClienteDni!) is null)
            throw new RecursoNoEncontradoException("El cliente debe registrarse antes de reservar.");

        var mesa = ObtenerMesaExistente(numeroMesa);
        ValidarCapacidad(mesa, cantidadPersonas);
        ValidarCruceHorarios(numeroMesa, fechaHora, null);

        var reserva = new Reserva(
            reservaRepositorio.GenerarSiguienteId(),
            solicitud.ClienteDni!,
            numeroMesa,
            fechaHora,
            cantidadPersonas);

        reservaRepositorio.Agregar(reserva);

        return Mapear(reserva);
        }
    }

    public ReservaResponse Actualizar(int id, ActualizarReservaRequest solicitud)
    {
        lock (BloqueoReservas.Candado)
        {
        var reserva = ObtenerReservaExistente(id);
        var fechaHora = solicitud.FechaHora!.Value;
        var cantidadPersonas = solicitud.CantidadPersonas!.Value;

        ValidarFechaFutura(fechaHora);

        var mesa = ObtenerMesaExistente(reserva.NumeroMesa);
        ValidarCapacidad(mesa, cantidadPersonas);
        ValidarCruceHorarios(reserva.NumeroMesa, fechaHora, reserva.Id);

        reserva.Actualizar(fechaHora, cantidadPersonas);
        reservaRepositorio.Actualizar(reserva);

        return Mapear(reserva);
        }
    }

    public void Cancelar(int id)
    {
        lock (BloqueoReservas.Candado)
        {
        var reserva = ObtenerReservaExistente(id);

        reserva.Cancelar();
        reservaRepositorio.Actualizar(reserva);
        }
    }

    public ReservaResponse Finalizar(int id)
    {
        lock (BloqueoReservas.Candado)
        {
            var reserva = ObtenerReservaExistente(id);
            reserva.Finalizar();
            reservaRepositorio.Actualizar(reserva);
            return Mapear(reserva);
        }
    }

    private Reserva ObtenerReservaExistente(int id) =>
        reservaRepositorio.BuscarPorId(id)
        ?? throw new RecursoNoEncontradoException($"No se encontró la reserva con Id {id}.");

    private Mesa ObtenerMesaExistente(int numeroMesa) =>
        mesaRepositorio.BuscarPorNumero(numeroMesa)
        ?? throw new RecursoNoEncontradoException($"La mesa número {numeroMesa} no existe.");

    private static void ValidarFechaFutura(DateTime fechaHora)
    {
        if (fechaHora <= DateTime.Now)
            throw new ReglaNegocioException("La fecha y hora de la reserva deben ser posteriores al momento actual.");
    }

    private static void ValidarCapacidad(Mesa mesa, int cantidadPersonas)
    {
        if (cantidadPersonas > mesa.Capacidad)
            throw new ReglaNegocioException(
                $"La mesa {mesa.Numero} tiene capacidad para {mesa.Capacidad} personas y se solicitaron {cantidadPersonas}.");
    }

    private void ValidarCruceHorarios(int numeroMesa, DateTime fechaHora, int? idExcluido)
    {
        var hayCruce = reservaRepositorio.ListarTodas().Any(r =>
            r.NumeroMesa == numeroMesa
            && r.Id != idExcluido
            && r.Estado == Reserva.EstadoConfirmada
            && Math.Abs((r.FechaHora - fechaHora).TotalMinutes) < MinutosMinimosEntreReservas);

        if (hayCruce)
            throw new ConflictoException(
                $"La mesa {numeroMesa} ya tiene una reserva en un rango menor a {MinutosMinimosEntreReservas} minutos.");
    }

    private static ReservaResponse Mapear(Reserva reserva) =>
        new(
            reserva.Id,
            reserva.ClienteDni,
            reserva.NumeroMesa,
            reserva.FechaHora,
            reserva.CantidadPersonas,
            reserva.Estado);
}
