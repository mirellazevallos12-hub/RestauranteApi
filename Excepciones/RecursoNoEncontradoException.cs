namespace RestauranteApi.Excepciones;

// Se lanza cuando algo que se busca no existe. El middleware responde error 404.
public class RecursoNoEncontradoException : Exception
{
    public RecursoNoEncontradoException(string mensaje)
        : base(mensaje)
    {
    }
}
