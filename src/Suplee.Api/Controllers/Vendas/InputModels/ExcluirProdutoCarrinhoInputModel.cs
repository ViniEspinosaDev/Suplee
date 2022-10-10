using System;

namespace Suplee.Api.Controllers.Vendas.InputModels
{
    public class ExcluirProdutoCarrinhoInputModel
    {
        public Guid UsuarioId { get; set; }
        public Guid ProdutoId { get; set; }
    }
}
