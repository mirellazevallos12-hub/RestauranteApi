using RestauranteApi.Modelos;

namespace RestauranteApi.Repositorios;

// Operaciones para consultar las mesas.
public interface IRepositorioMesas
{
    List<Mesa> ObtenerTodas();
    Mesa? ObtenerPorNumero(int numero);
}
