using System.ComponentModel.DataAnnotations;

namespace RestauranteApi.DTOs;

public class ActualizarReservaRequest
{
    [Required(ErrorMessage = "La fecha y hora de la reserva son obligatorias.")]
    public DateTime? FechaHora { get; set; }

    [Required(ErrorMessage = "La cantidad de personas es obligatoria.")]
    [Range(1, 12, ErrorMessage = "La cantidad de personas debe estar entre 1 y 12.")]
    public int? CantidadPersonas { get; set; }
}
