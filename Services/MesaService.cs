using RestauranteApi.DTOs;
using RestauranteApi.Exceptions;
using RestauranteApi.Models;
using RestauranteApi.Repositories;
namespace RestauranteApi.Services;
public class MesaService(IMesaRepository mesas, IReservaRepository reservas) : IMesaService
{
    public IEnumerable<MesaResponse> Listar(DateTime? fechaHora, int? personas)
    {
        if (personas is < 1 or > 12)
            throw new ReglaNegocioException("Personas debe estar entre 1 y 12.");
        if (fechaHora.HasValue && fechaHora.Value <= DateTime.Now)
            throw new ReglaNegocioException("La fecha consultada debe ser futura.");
        lock (BloqueoReservas.Candado)
        {
            var reservasRegistradas = reservas.ListarTodas();
            return mesas.ListarTodas()
                .Where(m => !personas.HasValue || m.Capacidad >= personas.Value)
                .Select(m => new MesaResponse(m.Numero, m.Capacidad,
                    !fechaHora.HasValue || !reservasRegistradas.Any(r =>
                        r.NumeroMesa == m.Numero && r.Estado == Reserva.EstadoConfirmada &&
                        Math.Abs((r.FechaHora - fechaHora.Value).TotalMinutes) < 120)))
                .ToList();
        }
    }
    public MesaResponse Obtener(int numero, DateTime? fechaHora)
    {
        var mesa = mesas.BuscarPorNumero(numero)
            ?? throw new RecursoNoEncontradoException("La mesa solicitada no existe.");
        return Listar(fechaHora, null).First(m => m.Numero == numero);
    }
}
