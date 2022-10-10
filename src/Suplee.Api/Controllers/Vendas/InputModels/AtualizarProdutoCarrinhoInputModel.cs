using System;

namespace Suplee.Api.Controllers.Vendas.InputModels
{
    public class AtualizarProdutoCarrinhoInputModel
    {
        public Guid UsuarioId { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
