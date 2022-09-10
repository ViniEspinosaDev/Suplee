using System;
using System.Collections.Generic;

namespace Suplee.Pagamentos.Domain.Models
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}