using Suplee.Core.DomainObjects;
using System;

namespace Suplee.Catalogo.Domain.Models
{
    public class ProdutoImagem : Entity
    {
        protected ProdutoImagem()
        {

        }

        public ProdutoImagem(Guid produtoId, string imagem)
        {
            ProdutoId = produtoId;
            Imagem = imagem;
        }

        public Guid ProdutoId { get; protected set; }
        public string Imagem { get; protected set; }

        public Produto Produto { get; protected set; }
    }
}
