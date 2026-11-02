using System.ComponentModel.DataAnnotations;

namespace RestauranteApi.Dtos;

// Datos que se envían en el POST para crear una reserva.
public class CrearReservaRequest
{
    [Required(ErrorMessage = "El DNI del cliente es obligatorio.")]
    [RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI debe tener exactamente 8 números.")]
    public string ClienteDni { get; set; } = "";

    [Required(ErrorMessage = "El número de mesa es obligatorio.")]
    [Range(1, 100, ErrorMessage = "El número de mesa debe estar entre 1 y 100.")]
    public int? NumeroMesa { get; set; }

    [Required(ErrorMessage = "La fecha y hora son obligatorias.")]
    public DateTime? FechaHora { get; set; }

    [Required(ErrorMessage = "La cantidad de personas es obligatoria.")]
    [Range(1, 12, ErrorMessage = "La cantidad de personas debe estar entre 1 y 12.")]
    public int? CantidadPersonas { get; set; }
}
