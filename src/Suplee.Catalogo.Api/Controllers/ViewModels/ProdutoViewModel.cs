using System;
using System.Collections.Generic;

namespace Suplee.Catalogo.Api.Controllers.ViewModels
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Composicao { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public decimal Preco { get; set; }
        public Guid CategoriaId { get; set; }
        public List<string> Imagens { get; set; }
        public List<Guid> Efeitos { get; set; }
        public InformacaoNutricionalViewModel InformacaoNutricional { get; set; }
    }

    public class InformacaoNutricionalViewModel
    {
        public string Cabecalho { get; set; }
        public string Legenda { get; set; }

        public List<CompostoNutricionalViewModel> CompostosNutricionais { get; set; }
    }

    public class CompostoNutricionalViewModel
    {
        public string Composto { get; set; }
        public string Porcao { get; set; }
        public string ValorDiario { get; set; }
        public int Ordem { get; set; }
    }
}
