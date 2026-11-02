using RestauranteApi.Modelos;

namespace RestauranteApi.Repositorios;

// Guarda las reservas en una lista en memoria (sin base de datos).
public class RepositorioReservasMemoria : IRepositorioReservas
{
    private readonly List<Reserva> reservas = new List<Reserva>();
    private int siguienteId = 1;

    public List<Reserva> ObtenerTodas()
    {
        return reservas.ToList();
    }

    public Reserva? ObtenerPorId(int id)
    {
        return reservas.FirstOrDefault(r => r.Id == id);
    }

    public Reserva Agregar(Reserva reserva)
    {
        reserva.Id = siguienteId;
        siguienteId = siguienteId + 1;
        reservas.Add(reserva);
        return reserva;
    }
}
