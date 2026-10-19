namespace RestauranteApi.DTOs;

public record ReservaResponse(
    int Id,
    string ClienteDni,
    int NumeroMesa,
    DateTime FechaHora,
    int CantidadPersonas,
    string Estado);
