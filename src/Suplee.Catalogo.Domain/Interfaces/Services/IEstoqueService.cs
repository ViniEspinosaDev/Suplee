using Suplee.Core.DomainObjects.DTO;
using System;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Interfaces.Services
{
    public interface IEstoqueService : IDisposable
    {
        Task<bool> DebitarEstoque(Guid produtoId, int quantidade);
        Task<bool> DebitarListaProdutosPedido(PedidoDomainObject pedido);
        Task<bool> ReporEstoque(Guid produtoId, int quantidade);
        Task<bool> ReporListaProdutosPedido(PedidoDomainObject pedido);
    }
}
