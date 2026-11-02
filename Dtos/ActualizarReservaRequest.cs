using System.ComponentModel.DataAnnotations;

namespace RestauranteApi.Dtos;

// Datos que se envían en el PUT para cambiar una reserva.
public class ActualizarReservaRequest
{
    [Required(ErrorMessage = "La fecha y hora son obligatorias.")]
    public DateTime? FechaHora { get; set; }

    [Required(ErrorMessage = "La cantidad de personas es obligatoria.")]
    [Range(1, 12, ErrorMessage = "La cantidad de personas debe estar entre 1 y 12.")]
    public int? CantidadPersonas { get; set; }
}
