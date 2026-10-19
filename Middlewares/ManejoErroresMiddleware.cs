using RestauranteApi.DTOs;
using RestauranteApi.Exceptions;

namespace RestauranteApi.Middlewares;

public class ManejoErroresMiddleware(RequestDelegate siguiente, ILogger<ManejoErroresMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext contexto)
    {
        try
        {
            await siguiente(contexto);
        }
        catch (RecursoNoEncontradoException ex)
        {
            await ResponderAsync(contexto, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (ConflictoException ex)
        {
            await ResponderAsync(contexto, StatusCodes.Status409Conflict, ex.Message);
        }
        catch (ReglaNegocioException ex)
        {
            await ResponderAsync(contexto, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error no controlado al procesar la solicitud.");
            await ResponderAsync(contexto, StatusCodes.Status500InternalServerError,
                "Ocurrió un error interno en el servidor.");
        }
    }

    private static async Task ResponderAsync(HttpContext contexto, int codigoEstado, string mensaje)
    {
        ErrorResponse error = new(codigoEstado, mensaje, DateTime.Now);

        contexto.Response.Clear();
        contexto.Response.StatusCode = codigoEstado;

        await contexto.Response.WriteAsJsonAsync(error);
    }
}
