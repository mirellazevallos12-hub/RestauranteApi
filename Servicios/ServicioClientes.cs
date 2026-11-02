using RestauranteApi.Dtos;
using RestauranteApi.Excepciones;
using RestauranteApi.Modelos;
using RestauranteApi.Repositorios;

namespace RestauranteApi.Servicios;

// Reglas para los clientes:
//  - No se puede registrar dos veces el mismo DNI.
//  - No se puede eliminar un cliente que ya tiene reservas.
public class ServicioClientes : IServicioClientes
{
    private readonly IRepositorioClientes clientes;
    private readonly IRepositorioReservas reservas;

    public ServicioClientes(IRepositorioClientes clientes, IRepositorioReservas reservas)
    {
        this.clientes = clientes;
        this.reservas = reservas;
    }

    public List<Cliente> ObtenerTodos()
    {
        return clientes.ObtenerTodos();
    }

    public Cliente ObtenerPorDni(string dni)
    {
        Cliente? cliente = clientes.ObtenerPorDni(dni);
        if (cliente == null)
        {
            throw new RecursoNoEncontradoException("No existe un cliente con ese DNI.");
        }
        return cliente;
    }

    public Cliente Crear(CrearClienteRequest peticion)
    {
        if (clientes.ObtenerPorDni(peticion.Dni) != null)
        {
            throw new ConflictoException("Ya existe un cliente con ese DNI.");
        }

        Cliente cliente = new Cliente();
        cliente.Dni = peticion.Dni;
        cliente.NombreCompleto = peticion.NombreCompleto.Trim();
        cliente.Telefono = peticion.Telefono.Trim();

        clientes.Agregar(cliente);
        return cliente;
    }

    public Cliente Actualizar(string dni, ActualizarClienteRequest peticion)
    {
        Cliente cliente = ObtenerPorDni(dni);

        cliente.NombreCompleto = peticion.NombreCompleto.Trim();
        cliente.Telefono = peticion.Telefono.Trim();
        return cliente;
    }

    public void Eliminar(string dni)
    {
        ObtenerPorDni(dni); // Si no existe, lanza el error 404.

        bool tieneReservas = reservas.ObtenerTodas().Any(r => r.ClienteDni == dni);
        if (tieneReservas)
        {
            throw new ConflictoException("No se puede eliminar un cliente que tiene reservas.");
        }

        clientes.Eliminar(dni);
    }
}
