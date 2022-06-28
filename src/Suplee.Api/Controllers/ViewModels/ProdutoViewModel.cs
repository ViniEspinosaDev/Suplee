using System;
using System.Collections.Generic;

namespace Suplee.Catalogo.Api.Controllers.ViewModels
{
    /// <summary>
    /// Objeto de saída do Produto
    /// </summary>
    public class ProdutoViewModel
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Composição
        /// </summary>
        public string Composicao { get; set; }

        /// <summary>
        /// Quantidade em estoque
        /// </summary>
        public int QuantidadeDisponivel { get; set; }

        /// <summary>
        /// Preço
        /// </summary>
        public decimal Preco { get; set; }

        /// <summary>
        /// Id da Categoria
        /// </summary>
        public Guid CategoriaId { get; set; }

        /// <summary>
        /// Imagens em Base64
        /// </summary>
        public List<string> Imagens { get; set; }

        /// <summary>
        /// Lista de Id dos Efeitos
        /// </summary>
        public List<Guid> Efeitos { get; set; }

        /// <summary>
        /// Tabela de Informação Nutricional
        /// </summary>
        public InformacaoNutricionalViewModel InformacaoNutricional { get; set; }
    }

    /// <summary>
    /// Objeto de saída da Informação Nutricional
    /// </summary>
    public class InformacaoNutricionalViewModel
    {
        /// <summary>
        /// Ex: Porção de 50g (1 unidade)
        /// </summary>
        public string Cabecalho { get; set; }

        /// <summary>
        /// Exemplo: "% Valores Diários com base em uma dieta de 2000Kcal ou 8400KJ. Seus valores diários..."
        /// </summary>
        public string Legenda { get; set; }

        /// <summary>
        /// Linhas com Compostos Nutricionais. Ex: Carboidrato | 19g | 2
        /// </summary>
        public List<CompostoNutricionalViewModel> CompostosNutricionais { get; set; }
    }

    /// <summary>
    /// Objeto de saída do Composto Nutricional
    /// </summary>
    public class CompostoNutricionalViewModel
    {
        /// <summary>
        /// Exemplo: Vitamina B12, Valor energético, Sódio
        /// </summary>
        public string Composto { get; set; }

        /// <summary>
        /// Exemplo: 200 Kcal= 900 KJ, 19g
        /// </summary>
        public string Porcao { get; set; }

        /// <summary>
        /// Exemplo: 9, 6, 2
        /// </summary>
        public string ValorDiario { get; set; }

        /// <summary>
        /// Informar a ordem na Tabela Nutricional
        /// </summary>
        public int Ordem { get; set; }
    }
}
