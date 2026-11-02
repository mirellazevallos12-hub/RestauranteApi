namespace RestauranteApi.Modelos;

// Reserva de una mesa hecha por un cliente.
public class Reserva
{
    public int Id { get; set; }
    public string ClienteDni { get; set; } = "";
    public int NumeroMesa { get; set; }
    public DateTime FechaHora { get; set; }
    public int CantidadPersonas { get; set; }

    // Puede ser "Confirmada", "Cancelada" o "Finalizada".
    public string Estado { get; set; } = "Confirmada";
}
