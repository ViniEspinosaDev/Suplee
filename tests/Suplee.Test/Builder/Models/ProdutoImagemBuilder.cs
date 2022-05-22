using Suplee.Catalogo.Domain.Models;
using System;

namespace Suplee.Test.Builder.Models
{
    public class ProdutoImagemBuilder : ProdutoImagem
    {
        public ProdutoImagemBuilder(
            Guid produtoId = default,
            string nomeImagem = default,
            string url = default) : base(produtoId, nomeImagem, url)
        {
        }

        public ProdutoImagemBuilder PadraoValido()
        {
            NomeImagem = "Imagem";
            Url = "www.imagem.com";

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
