using Suplee.Catalogo.Domain.Models;
using Suplee.Core.Messages.CommonMessages.DomainEvents;

namespace Suplee.Catalogo.Domain.Events
{
    public class ProdutoAdicionadoEvent : DomainEvent
    {
        public ProdutoAdicionadoEvent(Produto produto) : base(produto.Id)
        {
            Produto = produto;
        }

        public Produto Produto { get; protected set; }
    }
}
