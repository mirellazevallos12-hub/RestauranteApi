using RestauranteApi.Models;

namespace RestauranteApi.Repositories;

public class MesaRepositoryMemoria : IMesaRepository
{
    private readonly List<Mesa> _mesas =
    [
        new(1, 2),
        new(2, 4),
        new(3, 4),
        new(4, 6),
        new(5, 8)
    ];

    public List<Mesa> ListarTodas() => [.. _mesas];

    public Mesa? BuscarPorNumero(int numero) =>
        _mesas.FirstOrDefault(m => m.Numero == numero);
}
