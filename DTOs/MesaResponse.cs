namespace RestauranteApi.DTOs;
// Disponible significa que no hay una reserva confirmada en el intervalo consultado.
public record MesaResponse(int Numero, int Capacidad, bool Disponible);
