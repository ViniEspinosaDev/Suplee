using MediatR;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages;
using Suplee.Core.Messages.CommonMessages.Events;
using Suplee.Vendas.Domain.Enums;
using Suplee.Vendas.Domain.Interfaces;
using Suplee.Vendas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Suplee.Vendas.Domain.Models.Pedido;

namespace Suplee.Vendas.Domain.Commands
{
    public class PedidoCommandHandler : CommandHandler,
        IRequestHandler<InserirProdutoCarrinhoCommand, bool>,
        IRequestHandler<ExcluirProdutoCarrinhoCommand, bool>,
        IRequestHandler<AtualizarProdutoCarrinhoCommand, bool>,
        IRequestHandler<IniciarPedidoCommand, bool>
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

            var pedido = await _pedidoRepository.ObterCarrinhoPorUsuarioId(request.UsuarioId);

            var pedidoProduto = new PedidoProduto(request.ProdutoId, request.NomeProduto, request.Quantidade, request.ValorUnitario);

            if (pedido == null)
            {
                pedido = PedidoFactory.NovoPedidoRascunho(request.UsuarioId);

                pedido.AdicionarProduto(pedidoProduto);

                _pedidoRepository.Adicionar(pedido);
            }
            else
            {
                if (pedido.Status != EPedidoStatus.Rascunho)
                {
                    await NotificarErro(request, "É necessário o pedido ser um rascunho para inserir produto");
                    return false;
                }

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

            return sucesso;
        }

        public async Task<bool> Handle(ExcluirProdutoCarrinhoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return default(bool);
            }

            var pedido = await _pedidoRepository.ObterCarrinhoPorUsuarioId(request.UsuarioId);

            if (pedido == null)
            {
                await NotificarErro(request, "O usuário não possui carrinho");
                return false;
            }

            var produto = pedido.Produtos.FirstOrDefault(x => x.ProdutoId == request.ProdutoId);

            if (produto == null)
            {
                await NotificarErro(request, "Carrinho não possui o produto para excluir");
                return false;
            }

            pedido.RemoverProduto(produto);

            var sucesso = await _pedidoRepository.UnitOfWork.Commit();

            return sucesso;
        }

        public async Task<bool> Handle(AtualizarProdutoCarrinhoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return default(bool);
            }

            var pedido = await _pedidoRepository.ObterCarrinhoPorUsuarioId(request.UsuarioId);

            if (pedido == null)
            {
                await NotificarErro(request, "O usuário não possui carrinho");
                return false;
            }

            var produto = pedido.Produtos.FirstOrDefault(x => x.ProdutoId == request.ProdutoId);

            if (produto == null)
            {
                await NotificarErro(request, "Carrinho não possui o produto para atualizar");
                return false;
            }

            produto.AtualizarQuantidade(request.Quantidade);

            pedido.AtualizarProduto(produto);

            var sucesso = await _pedidoRepository.UnitOfWork.Commit();

            return sucesso;
        }

        public async Task<bool> Handle(IniciarPedidoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return default(bool);
            }

            var pedido = await _pedidoRepository.ObterCarrinhoPorUsuarioId(request.UsuarioId);

            if (pedido == null)
            {
                await NotificarErro(request, "O usuário não possui carrinho");
                return false;
            }

            if (pedido.Produtos.Count == 0)
            {
                await NotificarErro(request, "O carrinho não possui produto(s)");
                return false;
            }

            if (!request.Sucesso)
            {
                await NotificarErro(request, "A operadora recusou o pagamento");
                return false;
            }

            pedido.Iniciar();

            var sucesso = await _pedidoRepository.UnitOfWork.Commit();

            if (sucesso)
            {
                var produtos = new List<(Guid produtoId, int quantidade)>();

                pedido.Produtos.ToList().ForEach(produto => produtos.Add((produtoId: produto.Id, quantidade: produto.Quantidade)));

                var pedidoIniciadoEvent = new PedidoIniciadoEvent(request.UsuarioId, pedido.Id, produtos);

                await _mediatorHandler.PublicarEvento(pedidoIniciadoEvent);
            }

            return sucesso;
        }
    }
}
