using Suplee.Catalogo.Domain.Events;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Catalogo.Domain.Interfaces.Services;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.DomainObjects.DTO;
using Suplee.Core.Messages.CommonMessages.Notifications;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public EstoqueService(IProdutoRepository produtoRepository,
                              IMediatorHandler mediatorHandler)
        {
            _produtoRepository = produtoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> DebitarListaProdutosPedido(PedidoDomainObject pedido)
        {
            foreach (var item in pedido.Produtos)
            {
                if (!await DebitarItemEstoque(item.Id, item.Quantidade)) return false;
            }

            var sucesso = await _produtoRepository.UnitOfWork.Commit();

            if (sucesso)
            {
                //await _mediatorHandler.PublicarDomainEvent(new ProdutosEstoqueDebitadoEvent(pedido.Produtos.ToList()));
            }

            return sucesso;
        }

        private async Task<bool> DebitarItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterProduto(produtoId);

            if (produto == null) return false;

            if (!produto.PossuiEstoque(quantidade))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("Estoque", $"Produto - {produto.Nome} sem estoque"));
                return false;
            }

            produto.DebitarEstoque(quantidade);

            return true;
        }

        public async Task<bool> ReporListaProdutosPedido(PedidoDomainObject pedido)
        {
            foreach (var item in pedido.Produtos)
            {
                await ReporItemEstoque(item.Id, item.Quantidade);
            }

            var sucesso = await _produtoRepository.UnitOfWork.Commit();

            if (sucesso)
            {
                //await _mediatorHandler.PublicarDomainEvent(new ProdutosEstoqueRepostoEvent(pedido.Produtos.ToList()));
            }

            return sucesso;
        }

        private async Task<bool> ReporItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterProduto(produtoId);

            if (produto == null) return false;

            produto.ReporEstoque(quantidade);

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}
