using B3.Api.DTO;
using B3.Api.Models;

namespace B3.Api.Interfaces
{
    public interface ICDBService
    {
        CDB Calcular(CalculoCDB calculo);
    }
}
