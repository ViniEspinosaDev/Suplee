using Suplee.Core.DomainObjects;
using System.Collections.Generic;

namespace Suplee.Vendas.Domain.Models
{
    public class Produto : Entity
    {
        public string Nome { get; protected set; }
        public List<ProdutoImagem> Imagens { get; protected set; }
    }
}
