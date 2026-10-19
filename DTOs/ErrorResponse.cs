namespace RestauranteApi.DTOs;

public record ErrorResponse(int CodigoEstado, string Mensaje, DateTime Fecha);
