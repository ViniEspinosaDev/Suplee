using Suplee.Core.DomainObjects;
using System;

namespace Suplee.Catalogo.Domain.Models
{
    public class ProdutoEfeito : Entity
    {
        protected ProdutoEfeito()
        {

        }

        public ProdutoEfeito(Guid produtoId, Guid efeitoId)
        {
            ProdutoId = produtoId;
            EfeitoId = efeitoId;
        }

        public Guid ProdutoId { get; protected set; }
        public Guid EfeitoId { get; protected set; }

        public Efeito Efeito { get; protected set; }
        public Produto Produto { get; protected set; }
    }
}
