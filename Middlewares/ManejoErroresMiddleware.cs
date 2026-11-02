using RestauranteApi.Excepciones;

namespace RestauranteApi.Middlewares;

// Atrapa cualquier error de la API y responde siempre con el mismo formato JSON:
// { "estado": 404, "mensaje": "..." }
public class ManejoErroresMiddleware
{
    private readonly RequestDelegate siguiente;

    public ManejoErroresMiddleware(RequestDelegate siguiente)
    {
        this.siguiente = siguiente;
    }

    public async Task InvokeAsync(HttpContext contexto)
    {
        try
        {
            // Deja pasar la petición hacia el controlador.
            await siguiente(contexto);
        }
        catch (RecursoNoEncontradoException ex)
        {
            await EscribirError(contexto, 404, ex.Message);
        }
        catch (ReglaNegocioException ex)
        {
            await EscribirError(contexto, 400, ex.Message);
        }
        catch (ConflictoException ex)
        {
            await EscribirError(contexto, 409, ex.Message);
        }
        catch (Exception)
        {
            await EscribirError(contexto, 500, "Ocurrió un error inesperado en el servidor.");
        }
    }

    private static async Task EscribirError(HttpContext contexto, int codigo, string mensaje)
    {
        contexto.Response.Clear();
        contexto.Response.StatusCode = codigo;
        await contexto.Response.WriteAsJsonAsync(new { estado = codigo, mensaje = mensaje });
    }
}
