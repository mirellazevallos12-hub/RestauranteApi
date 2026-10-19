using Microsoft.AspNetCore.Mvc;
using RestauranteApi.DTOs;
using RestauranteApi.Services;

namespace RestauranteApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController(IReservaService servicio) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<ReservaResponse>> Listar() =>
        Ok(servicio.ListarTodas());

    [HttpGet("{id:int}")]
    public ActionResult<ReservaResponse> ObtenerPorId(int id) =>
        Ok(servicio.ObtenerPorId(id));

    [HttpPost]
    public ActionResult<ReservaResponse> Crear(CrearReservaRequest solicitud)
    {
        var creada = servicio.Crear(solicitud);

        return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
    }

    [HttpPut("{id:int}")]
    public ActionResult<ReservaResponse> Actualizar(int id, ActualizarReservaRequest solicitud) =>
        Ok(servicio.Actualizar(id, solicitud));

    [HttpPatch("{id:int}/finalizar")]
    public ActionResult<ReservaResponse> Finalizar(int id) => Ok(servicio.Finalizar(id));

    [HttpDelete("{id:int}")]
    public IActionResult Cancelar(int id)
    {
        servicio.Cancelar(id);

        return NoContent();
    }
}
