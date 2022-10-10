using System;
using System.Collections.Generic;

namespace Suplee.Api.Controllers.Catalogo.ViewModels
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Composicao { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public decimal Preco { get; set; }
        public CategoriaViewModel Categoria { get; set; }
        public List<ProdutoImagemViewModel> Imagens { get; set; }
        public List<EfeitoViewModel> Efeitos { get; set; }
        public InformacaoNutricionalViewModel InformacaoNutricional { get; set; }
    }

    public class ProdutoImagemViewModel
    {
        public string NomeImagem { get; set; }
        public string UrlImagemOriginal { get; set; }
        public string UrlImagemReduzida { get; set; }
        public string UrlImagemMaior { get; set; }

    }

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
