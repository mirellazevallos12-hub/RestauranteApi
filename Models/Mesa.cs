using RestauranteApi.Exceptions;

namespace RestauranteApi.Models;

public class Mesa
{
    public int Numero { get; }
    public int Capacidad { get; private set; }
    public bool EstaDisponible { get; private set; }

    public Mesa(int numero, int capacidad)
    {
        if (numero <= 0)
            throw new ReglaNegocioException("El número de mesa debe ser mayor a 0.");

        if (capacidad < 1 || capacidad > 12)
            throw new ReglaNegocioException("La capacidad de la mesa debe estar entre 1 y 12 personas.");

        Numero = numero;
        Capacidad = capacidad;
        EstaDisponible = true;
    }

    public void Ocupar()
    {
        if (!EstaDisponible)
            throw new ReglaNegocioException($"La mesa {Numero} ya se encuentra ocupada.");

        EstaDisponible = false;
    }

    public void Liberar()
    {
        EstaDisponible = true;
    }
}
