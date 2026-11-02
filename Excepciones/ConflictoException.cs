namespace RestauranteApi.Excepciones;

// Se lanza cuando hay un choque con datos que ya existen
// (DNI repetido, mesa ya reservada a esa hora). El middleware responde error 409.
public class ConflictoException : Exception
{
    public ConflictoException(string mensaje)
        : base(mensaje)
    {
    }
}
