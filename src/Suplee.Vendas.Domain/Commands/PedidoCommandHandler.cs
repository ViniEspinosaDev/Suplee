using MediatR;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages;
using Suplee.Vendas.Domain.Interfaces;
using Suplee.Vendas.Domain.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Suplee.Vendas.Domain.Models.Pedido;

namespace Suplee.Vendas.Domain.Commands
{
    public class PedidoCommandHandler : CommandHandler,
        IRequestHandler<InserirProdutoCarrinhoCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoCommandHandler(IMediatorHandler mediatorHandler, IPedidoRepository pedidoRepository) : base(mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<bool> Handle(InserirProdutoCarrinhoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return default(bool);
            }

            var pedido = await _pedidoRepository.ObterPedidoPorUsuarioId(request.UsuarioId);
            var pedidoProduto = new PedidoProduto(request.ProdutoId, request.NomeProduto, request.Quantidade, request.ValorUnitario);

            if (pedido == null)
            {
                pedido = PedidoFactory.NovoPedidoRascunho(request.UsuarioId);

                pedido.AdicionarProduto(pedidoProduto);

                _pedidoRepository.Adicionar(pedido);
            }
            else
            {
                var existeProduto = pedido.ProdutoJaExiste(pedidoProduto);

                pedido.AdicionarProduto(pedidoProduto);

                if (existeProduto)
                {
                    _pedidoRepository.AtualizarPedidoProduto(pedido.Produtos.FirstOrDefault(p => p.ProdutoId == pedidoProduto.ProdutoId));
                }
                else
                {
                    _pedidoRepository.AdicionarPedidoProduto(pedidoProduto);
                }
            }

            var sucesso = await _pedidoRepository.UnitOfWork.Commit();

            // if(sucesso && pedido.Status == EPedidoStatus.Iniciado) await _mediatorHandler.PublicarEvento(new ProdutoInseridoPedidoEvent(pedidoProduto.ProdutoId, pedidoProduto.Quantidade));

            return sucesso;
        }
    }
}
