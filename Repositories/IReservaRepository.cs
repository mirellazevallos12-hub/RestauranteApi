using RestauranteApi.Models;

namespace RestauranteApi.Repositories;

public interface IReservaRepository
{
    List<Reserva> ListarTodas();
    Reserva? BuscarPorId(int id);
    void Agregar(Reserva reserva);
    void Actualizar(Reserva reserva);
    bool Eliminar(int id);
    int GenerarSiguienteId();
}
