using RestauranteApi.Models;
namespace RestauranteApi.Repositories;
public interface IClienteRepository
{
    List<Cliente> ListarTodas();
    Cliente? BuscarPorDni(string dni);
    void Agregar(Cliente cliente);
    bool Eliminar(string dni);
}
