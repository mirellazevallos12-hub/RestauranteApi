using RestauranteApi.Models;

namespace RestauranteApi.Repositories;

public interface IMesaRepository
{
    List<Mesa> ListarTodas();
    Mesa? BuscarPorNumero(int numero);
}
