namespace RestauranteApi.Modelos;

// Persona que hace una reserva en el restaurante.
public class Cliente
{
    public string Dni { get; set; } = "";
    public string NombreCompleto { get; set; } = "";
    public string Telefono { get; set; } = "";
}
