using Suplee.Catalogo.Domain.Models;
using System;

namespace Suplee.Test.Builder.Models
{
    public class ProdutoImagemBuilder : ProdutoImagem
    {
        public ProdutoImagemBuilder PadraoValido()
        {
            Imagem = "Imagem";

            return this;
        }

        public ProdutoImagemBuilder ComProdutoId(Guid produtoId)
        {
            ProdutoId = produtoId;

            return this;
        }

        public ProdutoImagem Build() => this;
    }
}
