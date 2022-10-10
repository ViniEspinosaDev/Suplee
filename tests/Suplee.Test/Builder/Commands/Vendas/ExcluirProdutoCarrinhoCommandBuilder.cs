using Suplee.Vendas.Domain.Commands;
using System;

namespace Suplee.Test.Builder.Commands.Vendas
{
    public class ExcluirProdutoCarrinhoCommandBuilder : ExcluirProdutoCarrinhoCommand
    {
        public ExcluirProdutoCarrinhoCommandBuilder(
            Guid usuarioId = default,
            Guid produtoId = default) : base(produtoId)
        {
        }

        public ExcluirProdutoCarrinhoCommandBuilder ComandoValido()
        {
            UsuarioId = Guid.NewGuid();
            ProdutoId = Guid.NewGuid();

            return this;
        }

        public ExcluirProdutoCarrinhoCommandBuilder ComandoInvalido()
        {
            UsuarioId = Guid.Empty;
            ProdutoId = Guid.Empty;

            return this;
        }

        public ExcluirProdutoCarrinhoCommandBuilder ComProdutoId(Guid produtoId)
        {
            ProdutoId = produtoId;

            return this;
        }

        public ExcluirProdutoCarrinhoCommand Build() => this;
    }
}
