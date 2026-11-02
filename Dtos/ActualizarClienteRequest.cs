using System.ComponentModel.DataAnnotations;

namespace RestauranteApi.Dtos;

// Datos que se envían en el PUT para cambiar un cliente (el DNI no se puede cambiar).
public class ActualizarClienteRequest
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    public string NombreCompleto { get; set; } = "";

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "El teléfono debe tener entre 6 y 20 caracteres.")]
    public string Telefono { get; set; } = "";
}
