using System;

namespace Suplee.Api.Controllers.Vendas.InputModels
{
    /// <summary>
    /// Atualizar produto do carrinho
    /// </summary>
    public class AtualizarProdutoCarrinhoInputModel
    {
        /// <summary>
        /// Usuário id
        /// </summary>
        public Guid UsuarioId { get; set; }
        /// <summary>
        /// Produto id
        /// </summary>
        public Guid ProdutoId { get; set; }
        /// <summary>
        /// Quantidade
        /// </summary>
        public int Quantidade { get; set; }
    }
}
