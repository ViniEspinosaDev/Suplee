using Suplee.Vendas.Domain.Commands;
using System;

namespace Suplee.Test.Builder.Commands.Vendas
{
    public class AtualizarProdutoCarrinhoCommandBuilder : AtualizarProdutoCarrinhoCommand
    {
        public AtualizarProdutoCarrinhoCommandBuilder(
            Guid usuarioId = default,
            Guid produtoId = default,
            int quantidade = default) : base(usuarioId, produtoId, quantidade)
        {
        }

        public AtualizarProdutoCarrinhoCommandBuilder ComandoValido()
        {
            UsuarioId = Guid.NewGuid();
            ProdutoId = Guid.NewGuid();
            Quantidade = 5;

            return this;
        }

        public AtualizarProdutoCarrinhoCommandBuilder ComandoInvalido()
        {
            UsuarioId = Guid.Empty;
            ProdutoId = Guid.Empty;
            Quantidade = 0;

            return this;
        }

        public AtualizarProdutoCarrinhoCommandBuilder ComProdutoId(Guid produtoId)
        {
            ProdutoId = produtoId;

            return this;
        }

        public AtualizarProdutoCarrinhoCommand Build() => this;
    }
}
