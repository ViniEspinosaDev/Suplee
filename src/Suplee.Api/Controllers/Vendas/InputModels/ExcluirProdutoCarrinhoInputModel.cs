using System;

namespace Suplee.Api.Controllers.Vendas.InputModels
{
    /// <summary>
    /// Excluir produto do carrinho
    /// </summary>
    public class ExcluirProdutoCarrinhoInputModel
    {
        /// <summary>
        /// Usuário id
        /// </summary>
        public Guid UsuarioId { get; set; }
        /// <summary>
        /// Produto id
        /// </summary>
        public Guid ProdutoId { get; set; }
    }
}
