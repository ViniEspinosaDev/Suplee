using MediatR;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.DomainObjects.DTO;
using Suplee.Core.Messages;
using Suplee.Core.Messages.CommonMessages.IntegrationEvents;
using Suplee.Vendas.Domain.Enums;
using Suplee.Vendas.Domain.Events;
using Suplee.Vendas.Domain.Interfaces;
using Suplee.Vendas.Domain.Models;
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
        IRequestHandler<IniciarPedidoCommand, bool>,
        IRequestHandler<CancelarProcessamentoPedidoCommand, bool>,
        IRequestHandler<FinalizarPedidoCommand, bool>,
        IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>,
        IRequestHandler<CadastrarCarrinhoCommand, bool>
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

            if (sucesso)
            {
                await _mediatorHandler.PublicarEvento(
                    new PedidoProdutoAdicionadoEvent(
                        request.UsuarioId,
                        pedidoProduto.PedidoId,
                        pedidoProduto.ProdutoId,
                        pedidoProduto.NomeProduto,
                        pedidoProduto.ValorUnitario,
                        pedidoProduto.Quantidade));
            }

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

            if (sucesso)
                await _mediatorHandler.PublicarEvento(new PedidoProdutoRemovidoEvent(request.UsuarioId, pedido.Id, produto.Id));

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

            if (sucesso)
                await _mediatorHandler.PublicarEvento(new PedidoProdutoAtualizadoEvent(request.UsuarioId, pedido.Id, request.ProdutoId, request.Quantidade));

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
                var produtosDomainObject = new List<ProdutoDomainObject>();

                pedido.Produtos.ToList().ForEach(i => produtosDomainObject.Add(new ProdutoDomainObject { Id = i.ProdutoId, Quantidade = i.Quantidade }));

                var pedidoDomainObject = new PedidoDomainObject { PedidoId = pedido.Id, Produtos = produtosDomainObject };

                var pedidoIniciadoEvent = new PedidoIniciadoEvent(request.UsuarioId, pedidoDomainObject);

                await _mediatorHandler.PublicarEvento(pedidoIniciadoEvent);
            }

            return sucesso;
        }

        public async Task<bool> Handle(CancelarProcessamentoPedidoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return default(bool);
            }

            var pedido = await _pedidoRepository.ObterPorId(request.PedidoId);

            if (pedido == null)
            {
                await NotificarErro(request, "Pedido não encontrado");
                return false;
            }

            pedido.TornarRascunho();

            return await _pedidoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(FinalizarPedidoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return default(bool);
            }

            var pedido = await _pedidoRepository.ObterPorId(request.PedidoId);

            if (pedido == null)
            {
                await NotificarErro(request, "Pedido não encontrado");
                return false;
            }

            pedido.Finalizar();

            var sucesso = await _pedidoRepository.UnitOfWork.Commit();

            if (sucesso)
                await _mediatorHandler.PublicarEvento(new PedidoFinalizadoEvent(request.PedidoId));

            return sucesso;
        }

        public async Task<bool> Handle(CancelarProcessamentoPedidoEstornarEstoqueCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return default(bool);
            }

            var pedido = await _pedidoRepository.ObterPorId(request.PedidoId);

            if (pedido == null)
            {
                await NotificarErro(request, "Pedido não encontrado");
                return false;
            }

            var produtosDomainObject = new List<ProdutoDomainObject>();

            pedido.Produtos.ToList().ForEach(i => produtosDomainObject.Add(new ProdutoDomainObject { Id = i.ProdutoId, Quantidade = i.Quantidade }));

            var pedidoDomainObject = new PedidoDomainObject { PedidoId = pedido.Id, Produtos = produtosDomainObject };

            pedido.TornarRascunho();

            var sucesso = await _pedidoRepository.UnitOfWork.Commit();

            if (sucesso)
                await _mediatorHandler.PublicarEvento(new PedidoProcessamentoCanceladoEvent(pedido.Id, pedido.UsuarioId, pedidoDomainObject));

            return sucesso;
        }

        public async Task<bool> Handle(CadastrarCarrinhoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotificarErrosValidacao(request);
                return default(bool);
            }

            var pedido = await _pedidoRepository.ObterCarrinhoPorUsuarioId(request.UsuarioId);

            var pedidoProdutos = new List<PedidoProduto>();

            request.Produtos.ForEach(produto =>
                pedidoProdutos.Add(new PedidoProduto(produto.ProdutoId, produto.NomeProduto, produto.Quantidade, produto.ValorUnitario)));

            if (pedido == null)
            {
                pedido = PedidoFactory.NovoPedidoRascunho(request.UsuarioId);

                pedido.AdicionarProdutos(pedidoProdutos);

                _pedidoRepository.Adicionar(pedido);
            }
            else
            {
                pedido.RemoverProdutos();

                pedido.AdicionarProdutos(pedidoProdutos);

                foreach (var pedidoProduto in pedidoProdutos)
                {
                    _pedidoRepository.AdicionarPedidoProduto(pedidoProduto);
                }
            }

            var sucesso = await _pedidoRepository.UnitOfWork.Commit();

            return sucesso;
        }
    }
}
