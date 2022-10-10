using System;

namespace Suplee.Api.Controllers.Vendas.InputModels
{
    public class InserirProdutoCarrinhoInputModel
    {
        public Guid? UsuarioId { get; set; }
        public Guid ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}
