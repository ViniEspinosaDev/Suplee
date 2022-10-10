using System;
using System.Collections.Generic;

namespace Suplee.Api.Controllers.Vendas.InputModels
{
    public class CadastrarCarrinhoInputModel
    {
        public Guid? UsuarioId { get; set; }
        public List<CarrinhoProdutoInputModel> Produtos { get; set; }
    }

    public class CarrinhoProdutoInputModel
    {
        public Guid ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}
