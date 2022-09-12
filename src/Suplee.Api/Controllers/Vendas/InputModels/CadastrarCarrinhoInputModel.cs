using System;
using System.Collections.Generic;

namespace Suplee.Api.Controllers.Vendas.InputModels
{
    /// <summary>
    ///  Carrinho input model
    /// </summary>
    public class CadastrarCarrinhoInputModel
    {
        /// <summary>
        /// Não precisa trazer do frontend pois é pego pelo Token
        /// </summary>
        public Guid? UsuarioId { get; set; }

        /// <summary>
        /// Produto
        /// </summary>
        public List<CarrinhoProdutoInputModel> Produtos { get; set; }
    }

    /// <summary>
    /// Produto para o carrinho
    /// </summary>
    public class CarrinhoProdutoInputModel
    {
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
