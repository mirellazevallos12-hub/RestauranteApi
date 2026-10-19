using RestauranteApi.Models;
namespace RestauranteApi.Repositories;
public class ClienteRepositoryMemoria : IClienteRepository
{
    private readonly List<Cliente> _clientes =
    [
        new("12345678", "Ana Torres", "987654321"),
        new("87654321", "Luis Ramos", "912345678")
    ];
    public List<Cliente> ListarTodas() => [.. _clientes];
    public Cliente? BuscarPorDni(string dni) => _clientes.FirstOrDefault(x => x.Dni == dni);
    public void Agregar(Cliente cliente) => _clientes.Add(cliente);
    public bool Eliminar(string dni) => _clientes.RemoveAll(c => c.Dni == dni) > 0;
}
