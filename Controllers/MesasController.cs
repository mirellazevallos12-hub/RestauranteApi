using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Modelos;
using RestauranteApi.Servicios;

namespace RestauranteApi.Controllers;

// Consulta de mesas. Ruta base: /api/mesas
[ApiController]
[Route("api/mesas")]
public class MesasController : ControllerBase
{
    private readonly IServicioMesas servicio;

    public MesasController(IServicioMesas servicio)
    {
        this.servicio = servicio;
    }

    // GET /api/mesas -> lista todas las mesas
    // GET /api/mesas?fechaHora=2030-12-10T20:00:00 -> solo las mesas libres a esa hora
    [HttpGet]
    public ActionResult<List<Mesa>> Listar([FromQuery] DateTime? fechaHora)
    {
        return Ok(servicio.Listar(fechaHora));
    }

    // GET /api/mesas/1 -> busca una mesa por su número
    [HttpGet("{numero}")]
    public ActionResult<Mesa> ObtenerPorNumero(int numero)
    {
        return Ok(servicio.ObtenerPorNumero(numero));
    }
}
