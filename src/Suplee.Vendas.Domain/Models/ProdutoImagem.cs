using Suplee.Core.DomainObjects;
using System;

namespace Suplee.Vendas.Domain.Models
{
    public class ProdutoImagem : Entity
    {
        public ProdutoImagem(Guid produtoId, string nomeImagem, string urlImagemOriginal, string urlImagemReduzida, string urlImagemMaior)
        {
            ProdutoId = produtoId;
            NomeImagem = nomeImagem;
            UrlImagemOriginal = urlImagemOriginal;
            UrlImagemReduzida = urlImagemReduzida;
            UrlImagemMaior = urlImagemMaior;
        }

        public Guid ProdutoId { get; protected set; }
        public string NomeImagem { get; protected set; }
        public string UrlImagemOriginal { get; protected set; }
        public string UrlImagemReduzida { get; protected set; }
        public string UrlImagemMaior { get; protected set; }

        public Produto Produto { get; protected set; }
    }
}
