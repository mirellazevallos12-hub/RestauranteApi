using RestauranteApi.Modelos;

namespace RestauranteApi.Servicios;

// Acciones de negocio disponibles para las mesas.
public interface IServicioMesas
{
    // Si se envía fechaHora, solo devuelve las mesas libres a esa hora.
    List<Mesa> Listar(DateTime? fechaHora);
    Mesa ObtenerPorNumero(int numero);
}
