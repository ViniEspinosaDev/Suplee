using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Suplee.Catalogo.Api.Controllers.Catalogo.InputModels
{
    public class ProdutoInputModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Composicao { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public decimal Preco { get; set; }
        public decimal Profundidade { get; set; }
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public Guid CategoriaId { get; set; }
        public List<IFormFile> Imagens { get; set; }
        public List<Guid> Efeitos { get; set; }
        public InformacaoNutricionalInputModel InformacaoNutricional { get; set; }
    }

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
