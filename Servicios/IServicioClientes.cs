using RestauranteApi.Dtos;
using RestauranteApi.Modelos;

namespace RestauranteApi.Servicios;

// Acciones de negocio disponibles para los clientes.
public interface IServicioClientes
{
    List<Cliente> ObtenerTodos();
    Cliente ObtenerPorDni(string dni);
    Cliente Crear(CrearClienteRequest peticion);
    Cliente Actualizar(string dni, ActualizarClienteRequest peticion);
    void Eliminar(string dni);
}
