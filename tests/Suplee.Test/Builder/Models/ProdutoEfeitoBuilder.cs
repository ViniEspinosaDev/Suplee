using Suplee.Catalogo.Domain.Models;
using System;

namespace Suplee.Test.Builder.Models
{
    public class ProdutoEfeitoBuilder : ProdutoEfeito
    {
        public ProdutoEfeitoBuilder PadraoValido(Guid produtoId, Guid efeitoId)
        {
            ProdutoId = produtoId;
            EfeitoId = efeitoId;

            return this;
        }

        public ProdutoEfeitoBuilder ComEfeito(Efeito efeito)
        {
            EfeitoId = efeito.Id;
            Efeito = efeito;

            return this;
        }

        public ProdutoEfeito Build() => this;
    }
}
