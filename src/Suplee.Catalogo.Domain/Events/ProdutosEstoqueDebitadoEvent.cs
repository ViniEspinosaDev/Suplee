using Suplee.Core.DomainObjects.DTO;
using Suplee.Core.Messages.CommonMessages.DomainEvents;
using System;
using System.Collections.Generic;

namespace Suplee.Catalogo.Domain.Events
{
    public class ProdutosEstoqueDebitadoEvent : DomainEvent
    {
        public ProdutosEstoqueDebitadoEvent(List<ProdutoDomainObject> produtos) : base(new Guid())
        {
            Produtos = produtos;
        }

        public List<ProdutoDomainObject> Produtos { get; set; }
    }
}
