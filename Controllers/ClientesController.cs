using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Dtos;
using RestauranteApi.Modelos;
using RestauranteApi.Servicios;

namespace RestauranteApi.Controllers;

// Recibe las peticiones HTTP de clientes y se las pasa al servicio.
// Ruta base: /api/clientes
[ApiController]
[Route("api/clientes")]
public class ClientesController : ControllerBase
{
    private readonly IServicioClientes servicio;

    public ClientesController(IServicioClientes servicio)
    {
        this.servicio = servicio;
    }

    // GET /api/clientes -> lista todos los clientes
    [HttpGet]
    public ActionResult<List<Cliente>> ObtenerTodos()
    {
        return Ok(servicio.ObtenerTodos());
    }

    // GET /api/clientes/12345678 -> busca un cliente por DNI
    [HttpGet("{dni}")]
    public ActionResult<Cliente> ObtenerPorDni(string dni)
    {
        return Ok(servicio.ObtenerPorDni(dni));
    }

    // POST /api/clientes -> registra un cliente
    [HttpPost]
    public ActionResult<Cliente> Crear(CrearClienteRequest peticion)
    {
        Cliente nuevo = servicio.Crear(peticion);
        return CreatedAtAction(nameof(ObtenerPorDni), new { dni = nuevo.Dni }, nuevo);
    }

    // PUT /api/clientes/12345678 -> cambia nombre y teléfono
    [HttpPut("{dni}")]
    public ActionResult<Cliente> Actualizar(string dni, ActualizarClienteRequest peticion)
    {
        return Ok(servicio.Actualizar(dni, peticion));
    }

    // DELETE /api/clientes/12345678 -> elimina el cliente
    [HttpDelete("{dni}")]
    public IActionResult Eliminar(string dni)
    {
        servicio.Eliminar(dni);
        return NoContent();
    }
}
