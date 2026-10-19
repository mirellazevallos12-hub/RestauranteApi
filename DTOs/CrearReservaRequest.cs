using System.ComponentModel.DataAnnotations;

namespace RestauranteApi.DTOs;

public class CrearReservaRequest
{
    [Required(ErrorMessage = "El DNI del cliente es obligatorio.")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI debe contener exactamente 8 dígitos.")]
    public string? ClienteDni { get; set; }

    [Required(ErrorMessage = "El número de mesa es obligatorio.")]
    [Range(1, 100, ErrorMessage = "El número de mesa debe estar entre 1 y 100.")]
    public int? NumeroMesa { get; set; }

    [Required(ErrorMessage = "La fecha y hora de la reserva son obligatorias.")]
    public DateTime? FechaHora { get; set; }

    [Required(ErrorMessage = "La cantidad de personas es obligatoria.")]
    [Range(1, 12, ErrorMessage = "La cantidad de personas debe estar entre 1 y 12.")]
    public int? CantidadPersonas { get; set; }
}
