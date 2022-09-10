using Suplee.Core.Data;
using Suplee.Pagamentos.Domain.Models;

namespace Suplee.Pagamentos.Domain.Interfaces
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        void Adicionar(Pagamento pagamento);
        void AdicionarTransacao(Transacao transacao);
    }
}