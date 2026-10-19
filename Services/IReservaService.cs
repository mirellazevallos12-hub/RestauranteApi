using RestauranteApi.DTOs;

namespace RestauranteApi.Services;

public interface IReservaService
{
    IEnumerable<ReservaResponse> ListarTodas();
    ReservaResponse ObtenerPorId(int id);
    ReservaResponse Crear(CrearReservaRequest solicitud);
    ReservaResponse Actualizar(int id, ActualizarReservaRequest solicitud);
    void Cancelar(int id);
    ReservaResponse Finalizar(int id);
}
