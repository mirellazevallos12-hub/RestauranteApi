using Microsoft.AspNetCore.Mvc;
using RestauranteApi.Dtos;
using RestauranteApi.Modelos;
using RestauranteApi.Servicios;

namespace RestauranteApi.Controllers;

// Recibe las peticiones HTTP de reservas y se las pasa al servicio.
// Ruta base: /api/reservas
[ApiController]
[Route("api/reservas")]
public class ReservasController : ControllerBase
{
    private readonly IServicioReservas servicio;

    public ReservasController(IServicioReservas servicio)
    {
        this.servicio = servicio;
    }

    // GET /api/reservas -> lista todas las reservas
    [HttpGet]
    public ActionResult<List<Reserva>> ObtenerTodas()
    {
        return Ok(servicio.ObtenerTodas());
    }

    // GET /api/reservas/1 -> busca una reserva por id
    [HttpGet("{id}")]
    public ActionResult<Reserva> ObtenerPorId(int id)
    {
        return Ok(servicio.ObtenerPorId(id));
    }

    // POST /api/reservas -> registra una reserva
    [HttpPost]
    public ActionResult<Reserva> Crear(CrearReservaRequest peticion)
    {
        Reserva nueva = servicio.Crear(peticion);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = nueva.Id }, nueva);
    }

    // PUT /api/reservas/1 -> cambia la fecha y la cantidad de personas
    [HttpPut("{id}")]
    public ActionResult<Reserva> Actualizar(int id, ActualizarReservaRequest peticion)
    {
        return Ok(servicio.Actualizar(id, peticion));
    }

    // PATCH /api/reservas/1/finalizar -> marca la reserva como atendida
    [HttpPatch("{id}/finalizar")]
    public ActionResult<Reserva> Finalizar(int id)
    {
        return Ok(servicio.Finalizar(id));
    }

    // DELETE /api/reservas/1 -> cancela la reserva (estado "Cancelada")
    [HttpDelete("{id}")]
    public IActionResult Cancelar(int id)
    {
        servicio.Cancelar(id);
        return NoContent();
    }
}
