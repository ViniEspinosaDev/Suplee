using Suplee.Catalogo.Domain.Models;
using Suplee.Core.Messages.CommonMessages.DomainEvents;
using System;
using System.Collections.Generic;

namespace Suplee.Catalogo.Domain.Events
{
    public class ProdutoAdicionadoEvent : DomainEvent
    {
        public ProdutoAdicionadoEvent(Produto produto, Guid categoriaId, List<Guid> efeitos) : base(produto.Id)
        {
            Produto = produto;
            CategoriaId = categoriaId;
            Efeitos = efeitos;
        }

        public Produto Produto { get; protected set; }
        public Guid CategoriaId { get; set; }
        public List<Guid> Efeitos { get; set; }
    }
}
