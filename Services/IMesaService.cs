using RestauranteApi.DTOs;
namespace RestauranteApi.Services;
public interface IMesaService
{
    IEnumerable<MesaResponse> Listar(DateTime? fechaHora, int? personas);
    MesaResponse Obtener(int numero, DateTime? fechaHora);
}
