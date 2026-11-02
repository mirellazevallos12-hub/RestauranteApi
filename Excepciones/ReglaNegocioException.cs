namespace RestauranteApi.Excepciones;

// Se lanza cuando se rompe una regla del restaurante
// (fecha pasada, mesa muy pequeña, etc.). El middleware responde error 400.
public class ReglaNegocioException : Exception
{
    public ReglaNegocioException(string mensaje)
        : base(mensaje)
    {
    }
}
