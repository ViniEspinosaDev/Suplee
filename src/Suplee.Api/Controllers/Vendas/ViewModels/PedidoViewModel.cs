using Suplee.Api.Controllers.Catalogo.ViewModels;
using System;
using System.Collections.Generic;

namespace Suplee.Api.Controllers.Vendas.ViewModels
{
    public class PedidoViewModel
    {
        public string Codigo { get; set; }
        public string Status { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<ProdutoCarrinhoViewModel> Produtos { get; set; }
    }

    public class ProdutoCarrinhoViewModel
    {
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public List<ProdutoImagemViewModel> Imagens { get; set; }
    }
}
