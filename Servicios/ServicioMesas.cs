using RestauranteApi.Excepciones;
using RestauranteApi.Modelos;
using RestauranteApi.Repositorios;

namespace RestauranteApi.Servicios;

// Consulta de mesas. Una mesa está libre si no tiene una reserva confirmada
// con menos de 2 horas de diferencia a la hora consultada.
public class ServicioMesas : IServicioMesas
{
    private const int MinutosEntreReservas = 120;

    private readonly IRepositorioMesas mesas;
    private readonly IRepositorioReservas reservas;

    public ServicioMesas(IRepositorioMesas mesas, IRepositorioReservas reservas)
    {
        this.mesas = mesas;
        this.reservas = reservas;
    }

    public List<Mesa> Listar(DateTime? fechaHora)
    {
        List<Mesa> todas = mesas.ObtenerTodas();

        if (fechaHora == null)
        {
            return todas;
        }

        List<Mesa> libres = new List<Mesa>();
        foreach (Mesa mesa in todas)
        {
            if (EstaLibre(mesa.Numero, fechaHora.Value))
            {
                libres.Add(mesa);
            }
        }
        return libres;
    }

    public Mesa ObtenerPorNumero(int numero)
    {
        Mesa? mesa = mesas.ObtenerPorNumero(numero);
        if (mesa == null)
        {
            throw new RecursoNoEncontradoException("La mesa número " + numero + " no existe.");
        }
        return mesa;
    }

    private bool EstaLibre(int numeroMesa, DateTime fechaHora)
    {
        foreach (Reserva reserva in reservas.ObtenerTodas())
        {
            bool mismaMesa = reserva.NumeroMesa == numeroMesa;
            bool estaConfirmada = reserva.Estado == "Confirmada";
            double minutosDeDiferencia = Math.Abs((reserva.FechaHora - fechaHora).TotalMinutes);

            if (mismaMesa && estaConfirmada && minutosDeDiferencia < MinutosEntreReservas)
            {
                return false;
            }
        }
        return true;
    }
}
