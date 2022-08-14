using Suplee.Vendas.Domain.Commands;
using System;

namespace Suplee.Test.Builder.Commands.Vendas
{
    public class InserirProdutoCarrinhoCommandBuilder : InserirProdutoCarrinhoCommand
    {
        public InserirProdutoCarrinhoCommandBuilder(
            Guid usuarioId = default,
            Guid produtoId = default,
            string nomeProduto = default,
            int quantidade = default,
            decimal valorUnitario = default) : base(usuarioId, produtoId, nomeProduto, quantidade, valorUnitario)
        {
        }

        public InserirProdutoCarrinhoCommandBuilder ComandoValido()
        {
            UsuarioId = Guid.NewGuid();
            ProdutoId = Guid.NewGuid();
            NomeProduto = "Nome Produto";
            Quantidade = 10;
            ValorUnitario = 15.90m;

            return this;
        }

        public InserirProdutoCarrinhoCommandBuilder ComandoInvalido()
        {
            UsuarioId = Guid.Empty;
            ProdutoId = Guid.Empty;
            NomeProduto = string.Empty;
            Quantidade = -1;
            ValorUnitario = 0;

            return this;
        }

        public InserirProdutoCarrinhoCommand Build() => this;
    }
}
