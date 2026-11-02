using RestauranteApi.Modelos;

namespace RestauranteApi.Repositorios;

// Las mesas del restaurante ya vienen creadas dentro de la lista.
public class RepositorioMesasMemoria : IRepositorioMesas
{
    private readonly List<Mesa> mesas = new List<Mesa>();

    public RepositorioMesasMemoria()
    {
        mesas.Add(new Mesa { Numero = 1, Capacidad = 2 });
        mesas.Add(new Mesa { Numero = 2, Capacidad = 4 });
        mesas.Add(new Mesa { Numero = 3, Capacidad = 4 });
        mesas.Add(new Mesa { Numero = 4, Capacidad = 6 });
        mesas.Add(new Mesa { Numero = 5, Capacidad = 8 });
    }

    public List<Mesa> ObtenerTodas()
    {
        return mesas.ToList();
    }

    public Mesa? ObtenerPorNumero(int numero)
    {
        return mesas.FirstOrDefault(m => m.Numero == numero);
    }
}
