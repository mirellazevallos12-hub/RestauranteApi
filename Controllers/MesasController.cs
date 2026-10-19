using Microsoft.AspNetCore.Mvc;
using RestauranteApi.DTOs;
using RestauranteApi.Services;
namespace RestauranteApi.Controllers;
[ApiController]
[Route("api/mesas")]
public class MesasController(IMesaService servicio) : ControllerBase
{
    // Ejemplo: GET /api/mesas?fechaHora=2027-12-10T20:00:00&personas=4
    [HttpGet]
    public ActionResult<IEnumerable<MesaResponse>> Listar([FromQuery] DateTime? fechaHora, [FromQuery] int? personas)
        => Ok(servicio.Listar(fechaHora, personas));
    [HttpGet("{numero:int}")]
    public ActionResult<MesaResponse> Obtener(int numero, [FromQuery] DateTime? fechaHora) => Ok(servicio.Obtener(numero, fechaHora));
}
