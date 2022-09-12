using System;

namespace Suplee.Api.Controllers.Vendas.InputModels
{
    /// <summary>
    /// Produto carrinho
    /// </summary>
    public class InserirProdutoCarrinhoInputModel
    {
        /// <summary>
        /// Usuário id é recuperado do Token
        /// </summary>
        public Guid? UsuarioId { get; set; }
        /// <summary>
        /// Produto id
        /// </summary>
        public Guid ProdutoId { get; set; }
        /// <summary>
        /// Nome produto
        /// </summary>
        public string NomeProduto { get; set; }
        /// <summary>
        /// Quantidade
        /// </summary>
        public int Quantidade { get; set; }
        /// <summary>
        /// Valor unitário
        /// </summary>
        public decimal ValorUnitario { get; set; }
    }
}
