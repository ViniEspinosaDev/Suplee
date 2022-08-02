using Suplee.Catalogo.Domain.Models;
using System;

namespace Suplee.Test.Builder.Models
{
    public class ProdutoImagemBuilder : ProdutoImagem
    {
        public ProdutoImagemBuilder(
            Guid produtoId = default,
            string nomeImagem = default,
            string urlImagemOriginal = default,
            string urlImagemReduzida = default,
            string urlImagemMaior = default) : base(produtoId, nomeImagem, urlImagemOriginal, urlImagemReduzida, urlImagemMaior)
        {
        }

        public ProdutoImagemBuilder PadraoValido()
        {
            ProdutoId = Guid.NewGuid();
            NomeImagem = "Imagem";
            UrlImagemOriginal = "www.imagem.com";
            UrlImagemReduzida = "www.imagem.com";
            UrlImagemMaior = "www.imagem.com";

            return this;
        }

        public ProdutoImagemBuilder ComProduto(Produto produto)
        {
            ProdutoId = produto.Id;
            Produto = produto;

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
