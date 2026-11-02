using RestauranteApi.Modelos;

namespace RestauranteApi.Repositorios;

// Operaciones para guardar y buscar clientes.
public interface IRepositorioClientes
{
    List<Cliente> ObtenerTodos();
    Cliente? ObtenerPorDni(string dni);
    void Agregar(Cliente cliente);
    void Eliminar(string dni);
}
