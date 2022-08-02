using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Suplee.Catalogo.Api.Controllers.Catalogo.InputModels
{
    /// <summary>
    /// Entrada do Produto
    /// </summary>
    public class ProdutoInputModel
    {
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
        /// Medida de Profundidade da embalagem
        /// </summary>
        public decimal Profundidade { get; set; }

        /// <summary>
        /// Medida de Altura da embalagem
        /// </summary>
        public decimal Altura { get; set; }

        /// <summary>
        /// Medida de Largura da embalagem
        /// </summary>
        public decimal Largura { get; set; }

        /// <summary>
        /// Id da Categoria
        /// </summary>
        public Guid CategoriaId { get; set; }

        /// <summary>
        /// Imagens em Base64
        /// </summary>
        public IFormFileCollection Imagens { get; set; }

        /// <summary>
        /// Lista de Id dos Efeitos
        /// </summary>
        public List<Guid> Efeitos { get; set; }

        /// <summary>
        /// Tabela com Informação Nutricional
        /// </summary>
        public InformacaoNutricionalInputModel InformacaoNutricional { get; set; }
    }

    /// <summary>
    /// Entrada da Informacao nutricional
    /// </summary>
    public class InformacaoNutricionalInputModel
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
        public List<CompostoNutricionalInputModel> CompostosNutricionais { get; set; }
    }

    /// <summary>
    /// Entrada do Composto nutricional
    /// </summary>
    public class CompostoNutricionalInputModel
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
    }
}
