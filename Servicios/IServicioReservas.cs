using RestauranteApi.Dtos;
using RestauranteApi.Modelos;

namespace RestauranteApi.Servicios;

// Acciones de negocio disponibles para las reservas.
public interface IServicioReservas
{
    List<Reserva> ObtenerTodas();
    Reserva ObtenerPorId(int id);
    Reserva Crear(CrearReservaRequest peticion);
    Reserva Actualizar(int id, ActualizarReservaRequest peticion);
    Reserva Finalizar(int id);
    void Cancelar(int id);
}
