namespace RestauranteApi.Services;
// Un candado compartido: hace atómicas las comprobaciones y los cambios en memoria.
public static class BloqueoReservas
{
    public static readonly object Candado = new();
}
