using RestauranteApi.Modelos;

namespace RestauranteApi.Repositorios;

// Operaciones para guardar y buscar reservas.
public interface IRepositorioReservas
{
    List<Reserva> ObtenerTodas();
    Reserva? ObtenerPorId(int id);
    Reserva Agregar(Reserva reserva);
}
