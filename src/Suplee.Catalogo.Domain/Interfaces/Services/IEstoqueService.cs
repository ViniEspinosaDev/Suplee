using Suplee.Core.DomainObjects.DTO;
using System;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Interfaces.Services
{
    public interface IEstoqueService : IDisposable
    {
        Task<bool> DebitarListaProdutosPedido(PedidoDomainObject pedido);
        Task<bool> ReporListaProdutosPedido(PedidoDomainObject pedido);
    }
}
