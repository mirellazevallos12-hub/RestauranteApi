using RestauranteApi.Modelos;

namespace RestauranteApi.Repositorios;

// Guarda los clientes en una lista en memoria (sin base de datos).
public class RepositorioClientesMemoria : IRepositorioClientes
{
    private readonly List<Cliente> clientes = new List<Cliente>();

    public List<Cliente> ObtenerTodos()
    {
        return clientes.ToList();
    }

    public Cliente? ObtenerPorDni(string dni)
    {
        return clientes.FirstOrDefault(c => c.Dni == dni);
    }

    public void Agregar(Cliente cliente)
    {
        clientes.Add(cliente);
    }

    public void Eliminar(string dni)
    {
        Cliente? cliente = ObtenerPorDni(dni);
        if (cliente != null)
        {
            clientes.Remove(cliente);
        }
    }
}
