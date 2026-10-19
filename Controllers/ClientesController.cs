using Microsoft.AspNetCore.Mvc;
using RestauranteApi.DTOs;
using RestauranteApi.Models;
using RestauranteApi.Services;
namespace RestauranteApi.Controllers;
[ApiController]
[Route("api/clientes")]
public class ClientesController(IClienteService servicio) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Cliente>> Listar() => Ok(servicio.Listar());
    [HttpGet("{dni}")]
    public ActionResult<Cliente> Obtener(string dni) => Ok(servicio.Obtener(dni));
    [HttpPost]
    public ActionResult<Cliente> Crear(ClienteRequest solicitud)
    {
        var nuevo = servicio.Crear(solicitud);
        return CreatedAtAction(nameof(Obtener), new { dni = nuevo.Dni }, nuevo);
    }
    [HttpPut("{dni}")]
    public ActionResult<Cliente> Actualizar(string dni, ActualizarClienteRequest solicitud) => Ok(servicio.Actualizar(dni, solicitud));
    [HttpDelete("{dni}")]
    public IActionResult Eliminar(string dni) { servicio.Eliminar(dni); return NoContent(); }
}
