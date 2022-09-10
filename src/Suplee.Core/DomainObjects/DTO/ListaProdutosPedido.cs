using System;
using System.Collections.Generic;

namespace Suplee.Core.DomainObjects.DTO
{
    public class PedidoDomainObject
    {
        public Guid PedidoId { get; set; }
        public ICollection<ProdutoDomainObject> Produtos { get; set; }
    }

    public class ProdutoDomainObject
    {
        public Guid Id { get; set; }
        public int Quantidade { get; set; }
    }
}
