using System.ComponentModel.DataAnnotations;
namespace RestauranteApi.DTOs;
public class ClienteRequest
{
    [Required, RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI debe tener 8 números.")]
    public string? Dni { get; set; }
    [Required, StringLength(100, MinimumLength = 3)]
    public string? NombreCompleto { get; set; }
    [Required, StringLength(20, MinimumLength = 6)]
    public string? Telefono { get; set; }
}
public class ActualizarClienteRequest
{
    [Required, StringLength(100, MinimumLength = 3)]
    public string? NombreCompleto { get; set; }
    [Required, StringLength(20, MinimumLength = 6)]
    public string? Telefono { get; set; }
}
