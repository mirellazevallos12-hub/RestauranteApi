using RestauranteApi.DTOs;
using RestauranteApi.Models;
namespace RestauranteApi.Services;
public interface IClienteService
{
    IEnumerable<Cliente> Listar();
    Cliente Obtener(string dni);
    Cliente Crear(ClienteRequest solicitud);
    Cliente Actualizar(string dni, ActualizarClienteRequest solicitud);
    void Eliminar(string dni);
}
