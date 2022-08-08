﻿using System;
using System.Collections.Generic;

namespace Suplee.Api.Controllers.Catalogo.ViewModels
{
    /// <summary>
    /// Objeto de saída do Produto resumido
    /// </summary>
    public class ProdutoResumidoViewModel
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ProdutoResumidoViewModel()
        {
            NomeEfeito = new List<string>();
        }

        /// <summary>
        /// Identificador
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Preço
        /// </summary>
        public decimal Preco { get; set; }

        /// <summary>
        /// Nome da Categoria
        /// </summary>
        public string NomeCategoria { get; set; }

        /// <summary>
        /// Quantidade disponível
        /// </summary>
        public int QuantidadeDisponivel { get; set; }

        /// <summary>
        /// Lista de nomes dos Efeitos
        /// </summary>
        public List<string> NomeEfeito { get; set; }

        /// <summary>
        /// URL da Imagem principal
        /// </summary>
        public List<ProdutoImagemViewModel> Imagens { get; set; }
    }
}
