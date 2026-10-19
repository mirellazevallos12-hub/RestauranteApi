using RestauranteApi.DTOs;
using RestauranteApi.Exceptions;
using RestauranteApi.Models;
using RestauranteApi.Repositories;
namespace RestauranteApi.Services;
public class ClienteService(IClienteRepository clientes, IReservaRepository reservas) : IClienteService
{
    public IEnumerable<Cliente> Listar() => clientes.ListarTodas();
    public Cliente Obtener(string dni) => clientes.BuscarPorDni(dni)
        ?? throw new RecursoNoEncontradoException("No existe un cliente con ese DNI.");
    public Cliente Crear(ClienteRequest solicitud)
    {
        lock (BloqueoReservas.Candado)
        {
            if (clientes.BuscarPorDni(solicitud.Dni!) != null)
                throw new ConflictoException("Ya existe un cliente con ese DNI.");
            var cliente = new Cliente(solicitud.Dni!, solicitud.NombreCompleto!, solicitud.Telefono!);
            clientes.Agregar(cliente);
            return cliente;
        }
    }
    public Cliente Actualizar(string dni, ActualizarClienteRequest solicitud)
    {
        lock (BloqueoReservas.Candado)
        {
            var cliente = Obtener(dni);
            cliente.Actualizar(solicitud.NombreCompleto!, solicitud.Telefono!);
            return cliente;
        }
    }
    public void Eliminar(string dni)
    {
        lock (BloqueoReservas.Candado)
        {
            Obtener(dni);
            if (reservas.ListarTodas().Any(r => r.ClienteDni == dni))
                throw new ConflictoException("No se puede eliminar un cliente con historial de reservas.");
            clientes.Eliminar(dni);
        }
    }
}
