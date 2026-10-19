using RestauranteApi.Exceptions;

namespace RestauranteApi.Models;

public class Reserva
{
    public const string EstadoConfirmada = "Confirmada";
    public const string EstadoCancelada = "Cancelada";
    public const string EstadoFinalizada = "Finalizada";

    public int Id { get; }
    public string ClienteDni { get; private set; }
    public int NumeroMesa { get; private set; }
    public DateTime FechaHora { get; private set; }
    public int CantidadPersonas { get; private set; }
    public string Estado { get; private set; }

    public Reserva(int id, string clienteDni, int numeroMesa, DateTime fechaHora, int cantidadPersonas)
    {
        Id = id;
        ClienteDni = clienteDni;
        NumeroMesa = numeroMesa;
        FechaHora = fechaHora;
        CantidadPersonas = cantidadPersonas;
        Estado = EstadoConfirmada;
    }

    public void Actualizar(DateTime fechaHora, int cantidadPersonas)
    {
        if (Estado != EstadoConfirmada)
            throw new ReglaNegocioException($"No se puede modificar una reserva en estado '{Estado}'.");

        FechaHora = fechaHora;
        CantidadPersonas = cantidadPersonas;
    }

    public void Cancelar()
    {
        if (Estado == EstadoCancelada || Estado == EstadoFinalizada)
            throw new ReglaNegocioException($"No se puede cancelar una reserva que ya está '{Estado}'.");

        Estado = EstadoCancelada;
    }

    public void Finalizar()
    {
        if (Estado != EstadoConfirmada)
            throw new ReglaNegocioException($"No se puede finalizar una reserva en estado '{Estado}'.");

        Estado = EstadoFinalizada;
    }
}
