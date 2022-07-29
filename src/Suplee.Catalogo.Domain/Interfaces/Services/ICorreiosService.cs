using Suplee.Catalogo.Domain.DTO;
using System;

namespace Suplee.Catalogo.Domain.Interfaces.Services
{
    public interface ICorreiosService
    {
        FreteDTO CalcularFrete(Guid produtoId, string cep);
    }
}
