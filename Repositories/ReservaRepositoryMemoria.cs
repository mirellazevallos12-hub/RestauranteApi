using RestauranteApi.Models;

namespace RestauranteApi.Repositories;

public class ReservaRepositoryMemoria : IReservaRepository
{
    private readonly Lock _candado = new();
    private readonly List<Reserva> _reservas;
    private int _ultimoId = 2;

    public ReservaRepositoryMemoria()
    {
        var manana = DateTime.Today.AddDays(1);

        _reservas =
        [
            new(1, "12345678", 2, manana.AddHours(20), 3),
            new(2, "87654321", 4, manana.AddHours(21), 5)
        ];
    }

    public List<Reserva> ListarTodas()
    {
        lock (_candado)
        {
            return [.. _reservas];
        }
    }

    public Reserva? BuscarPorId(int id)
    {
        lock (_candado)
        {
            return _reservas.FirstOrDefault(r => r.Id == id);
        }
    }

    public void Agregar(Reserva reserva)
    {
        lock (_candado)
        {
            _reservas.Add(reserva);
        }
    }

    public void Actualizar(Reserva reserva)
    {
        lock (_candado)
        {
            var indice = _reservas.FindIndex(r => r.Id == reserva.Id);

            if (indice >= 0)
                _reservas[indice] = reserva;
        }
    }

    public bool Eliminar(int id)
    {
        lock (_candado)
        {
            return _reservas.RemoveAll(r => r.Id == id) > 0;
        }
    }

    public int GenerarSiguienteId()
    {
        lock (_candado)
        {
            _ultimoId++;
            return _ultimoId;
        }
    }
}
