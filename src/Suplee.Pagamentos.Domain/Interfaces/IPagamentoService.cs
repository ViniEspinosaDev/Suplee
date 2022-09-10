using Suplee.Core.DomainObjects.DTO;
using Suplee.Pagamentos.Domain.Models;
using System.Threading.Tasks;

namespace Suplee.Pagamentos.Domain.Interfaces
{
    public interface IPagamentoService
    {
        Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido);
    }
}