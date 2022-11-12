using MediatR;
using Suplee.Catalogo.Domain.DTO;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Catalogo.Domain.Interfaces.Repositories;
using Suplee.Catalogo.Domain.Interfaces.Services;
using Suplee.Catalogo.Domain.Models;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Messages.CommonMessages.IntegrationEvents;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Events
{
    public class CatalogoEventHandler :
        INotificationHandler<ProdutoAdicionadoEvent>,
        INotificationHandler<PedidoIniciadoEvent>,
        INotificationHandler<PedidoProcessamentoCanceladoEvent>,
        INotificationHandler<ProdutosEstoqueDebitadoEvent>,
        INotificationHandler<ProdutosEstoqueRepostoEvent>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IEstoqueService _estoqueService;
        private readonly IProdutoLeituraRepository _produtoLeituraRepository;

        public CatalogoEventHandler(IProdutoRepository produtoRepository, IMediatorHandler mediatorHandler, IEstoqueService estoqueService, IProdutoLeituraRepository produtoLeituraRepository)
        {
            _produtoRepository = produtoRepository;
            _mediatorHandler = mediatorHandler;
            _estoqueService = estoqueService;
            _produtoLeituraRepository = produtoLeituraRepository;
        }

        public async Task Handle(ProdutoAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            var categoria = await _produtoRepository.ObterCategoria(notification.Produto.CategoriaId);

            var todosEfeitos = await _produtoRepository.ObterEfeitos();

            var efeitos = todosEfeitos.Where(x => notification.Efeitos.Any(y => y == x.Id)).ToList();

            ProdutoDTO produtoDTO = MapearProdutoEscritaParaProdutoLeitura(notification.Produto, categoria, efeitos);

            _produtoLeituraRepository.AdicionarProduto(produtoDTO);
        }

        public async Task Handle(PedidoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            var result = await _estoqueService.DebitarListaProdutosPedido(notification.Pedido);

            if (result)
            {
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueConfirmadoEvent(notification.Pedido.PedidoId, notification.UsuarioId, notification.Pedido, notification.SucessoNaTransacao));
            }
            else
            {
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueRejeitadoEvent(notification.Pedido.PedidoId, notification.UsuarioId));
            }
        }

        public async Task Handle(PedidoProcessamentoCanceladoEvent notification, CancellationToken cancellationToken)
        {
            await _estoqueService.ReporListaProdutosPedido(notification.Pedido);
        }

        public async Task Handle(ProdutosEstoqueDebitadoEvent notification, CancellationToken cancellationToken)
        {
            foreach (var produto in notification.Produtos)
            {
                var produtoMongoDb = await _produtoLeituraRepository.RecuperarProduto(produto.Id);

                int quantidadeParaDescontar = produto.Quantidade;

                if (quantidadeParaDescontar < 0) quantidadeParaDescontar *= -1;

                produtoMongoDb.QuantidadeDisponivel -= quantidadeParaDescontar;

                _produtoLeituraRepository.AtualizarProduto(produtoMongoDb);
            }
        }

        public async Task Handle(ProdutosEstoqueRepostoEvent notification, CancellationToken cancellationToken)
        {
            foreach (var produto in notification.Produtos)
            {
                var produtoMongoDb = await _produtoLeituraRepository.RecuperarProduto(produto.Id);

                int quantidadeParaRepor = produto.Quantidade;

                if (quantidadeParaRepor < 0) quantidadeParaRepor *= -1;

                produtoMongoDb.QuantidadeDisponivel += quantidadeParaRepor;

                _produtoLeituraRepository.AtualizarProduto(produtoMongoDb);
            }
        }

        private ProdutoDTO MapearProdutoEscritaParaProdutoLeitura(Produto produto, Categoria categoria, List<Efeito> efeitos)
        {
            var imagensDTO = new List<ProdutoImagemDTO>();

            produto.Imagens.ToList().ForEach(imagem =>
            {
                imagensDTO.Add(new ProdutoImagemDTO()
                {
                    Id = imagem.Id,
                    NomeImagem = imagem.NomeImagem,
                    UrlImagemOriginal = imagem.UrlImagemOriginal,
                    UrlImagemReduzida = imagem.UrlImagemReduzida,
                    UrlImagemMaior = imagem.UrlImagemMaior
                });
            });

            var efeitosDTO = new List<EfeitoDTO>();

            efeitos.ForEach(efeito =>
            {
                efeitosDTO.Add(new EfeitoDTO() { Id = efeito.Id, Nome = efeito.Nome });
            });

            var compostosNutricionaisDTO = new List<CompostoNutricionalDTO>();

            produto.InformacaoNutricional.CompostosNutricionais
                .ToList().ForEach(compostoNutricional =>
                {
                    compostosNutricionaisDTO.Add(new CompostoNutricionalDTO()
                    {
                        Id = compostoNutricional.Id,
                        Composto = compostoNutricional.Composto,
                        Porcao = compostoNutricional.Porcao,
                        ValorDiario = compostoNutricional.ValorDiario,
                        Ordem = compostoNutricional.Ordem
                    });
                });

            var informacaoNutricionalDTO = new InformacaoNutricionalDTO()
            {
                Id = produto.InformacaoNutricional.Id,
                Cabecalho = produto.InformacaoNutricional.Cabecalho,
                Legenda = produto.InformacaoNutricional.Legenda,
                CompostosNutricionais = compostosNutricionaisDTO
            };

            return new ProdutoDTO()
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Composicao = produto.Composicao,
                QuantidadeDisponivel = produto.QuantidadeDisponivel,
                Preco = produto.Preco,
                CategoriaId = produto.CategoriaId,
                Categoria = new CategoriaDTO() { Id = categoria.Id, Nome = categoria.Nome, Descricao = categoria.Descricao },
                Imagens = imagensDTO,
                Efeitos = efeitosDTO,
                InformacaoNutricionalId = produto.InformacaoNutricionalId,
                InformacaoNutricional = informacaoNutricionalDTO
            };
        }
    }
}
