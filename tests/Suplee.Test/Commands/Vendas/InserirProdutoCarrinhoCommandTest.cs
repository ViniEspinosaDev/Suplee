using Moq;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Test.Builder.Commands.Vendas;
using Suplee.Test.Builder.Models;
using Suplee.Vendas.Domain.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Suplee.Test.Commands.Vendas
{
    public class InserirProdutoCarrinhoCommandTest : VendasCommandTestBase
    {
        #region Validações comando
        [Fact]
        public void Deve_Validar_Comando_Valido()
        {
            var comando = new InserirProdutoCarrinhoCommandBuilder().ComandoValido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Empty(resultadoValidacao.Errors);
        }

        [Fact]
        public void Deve_Validar_Comando_Invalido()
        {
            var comando = new InserirProdutoCarrinhoCommandBuilder().ComandoInvalido().Build();

            comando.IsValid();
            var resultadoValidacao = comando.ValidationResult;

            Assert.Equal(5, resultadoValidacao.Errors.Count);
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoUsuarioId"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoProdutoId"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoNomeProduto"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoQuantidade"));
            Assert.Contains(resultadoValidacao.Errors, v => v.PropertyName.Equals("ValidacaoValorUnitario"));
        }
        #endregion

        #region Validações negócio
        [Fact]
        public async Task Deve_Validar_Handler_Comando_Invalido()
        {
            var comando = new InserirProdutoCarrinhoCommandBuilder().ComandoInvalido().Build();

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.AtLeastOnce);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Validar_Se_Pedido_Rascunho_Antes_De_Inserir()
        {
            var pedido = new PedidoBuilder().PadraoValido().Iniciado().Build();

            var comando = new InserirProdutoCarrinhoCommandBuilder().ComandoValido().Build();

            pedido.AdicionarProduto(new PedidoProduto(comando.ProdutoId, comando.NomeProduto, comando.Quantidade, comando.ValorUnitario));

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(pedido));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Never);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.Is<DomainNotification>(x => x.Value == "É necessário o pedido ser um rascunho para inserir produto")), Times.AtLeastOnce);

            Assert.False(resultado);
        }

        [Fact]
        public async Task Deve_Criar_Novo_Pedido_Rascunho()
        {
            var comando = new InserirProdutoCarrinhoCommandBuilder().ComandoValido().Build();

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(default(Pedido)));
            _pedidoRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.Never);

            Assert.True(resultado);
        }

        [Fact]
        public async Task Deve_Atualizar_Produto_Pedido()
        {
            var pedido = new PedidoBuilder().PadraoValido().Build();

            var comando = new InserirProdutoCarrinhoCommandBuilder().ComandoValido().Build();

            pedido.AdicionarProduto(new PedidoProduto(comando.ProdutoId, comando.NomeProduto, comando.Quantidade, comando.ValorUnitario));

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(pedido));
            _pedidoRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.Never);

            Assert.True(resultado);
        }

        [Fact]
        public async Task Deve_Adicionar_Produto_Pedido_Existente()
        {
            var pedido = new PedidoBuilder().PadraoValido().Build();

            var comando = new InserirProdutoCarrinhoCommandBuilder().ComandoValido().Build();

            _pedidoRepository.Setup(x => x.ObterCarrinhoPorUsuarioId(It.IsAny<Guid>())).Returns(Task.FromResult(pedido));
            _pedidoRepository.Setup(x => x.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            var resultado = await _handler.Handle(comando, _cancellationToken);

            _pedidoRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
            _mediatorHandler.Verify(x => x.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.Never);

            Assert.True(resultado);
        }
        #endregion
    }
}
