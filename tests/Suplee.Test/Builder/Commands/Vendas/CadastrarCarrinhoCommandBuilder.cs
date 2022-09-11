using Suplee.Vendas.Domain.Commands;
using System;
using System.Collections.Generic;
using static Suplee.Vendas.Domain.Commands.CadastrarCarrinhoCommand;

namespace Suplee.Test.Builder.Commands.Vendas
{
    public class CadastrarCarrinhoCommandBuilder : CadastrarCarrinhoCommand
    {
        public CadastrarCarrinhoCommandBuilder(
            Guid usuarioId = default,
            List<CadastrarCarrinhoCommandProduto> produtos = default) : base(usuarioId, produtos)
        {
        }

        public CadastrarCarrinhoCommandBuilder ComandoValido()
        {
            UsuarioId = Guid.NewGuid();
            Produtos = new List<CadastrarCarrinhoCommandProduto>() { new CadastrarCarrinhoCommandProdutoBuilder().PadraoValido().Build() };

            return this;
        }

        public CadastrarCarrinhoCommandBuilder ComandoInvalido()
        {
            UsuarioId = Guid.Empty;
            Produtos = new List<CadastrarCarrinhoCommandProduto>() { new CadastrarCarrinhoCommandProdutoBuilder().PadraoInvalido().Build() };

            return this;
        }

        public CadastrarCarrinhoCommand Build() => this;
    }

    public class CadastrarCarrinhoCommandProdutoBuilder : CadastrarCarrinhoCommandProduto
    {
        public CadastrarCarrinhoCommandProdutoBuilder(
            Guid produtoId = default,
            string nomeProduto = default,
            int quantidade = default,
            decimal valorUnitario = default) : base(produtoId, nomeProduto, quantidade, valorUnitario)
        {
        }

        public CadastrarCarrinhoCommandProdutoBuilder PadraoValido()
        {
            ProdutoId = Guid.NewGuid();
            NomeProduto = "Nome Produto";
            Quantidade = 12;
            ValorUnitario = 15.00m;

            return this;
        }

        public CadastrarCarrinhoCommandProdutoBuilder PadraoInvalido()
        {
            ProdutoId = Guid.Empty;
            NomeProduto = "";
            Quantidade = 0;
            ValorUnitario = 0m;

            return this;
        }

        public CadastrarCarrinhoCommandProduto Build() => this;
    }
}
