using Suplee.Core.DomainObjects;
using System;

namespace Suplee.Catalogo.Domain.Models
{
    public class ProdutoImagem : Entity
    {
        protected ProdutoImagem() { }
        public ProdutoImagem(Guid produtoId, string nomeImagem, string url)
        {
            ProdutoId = produtoId;
            NomeImagem = nomeImagem;
            Url = url;
        }

        public Guid ProdutoId { get; protected set; }
        public string NomeImagem { get; protected set; }
        public string Url { get; protected set; }

        public Produto Produto { get; protected set; }
    }
}
