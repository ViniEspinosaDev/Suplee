using System;
using System.Collections.Generic;

namespace Suplee.Catalogo.Api.Controllers.InputModels
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
        public Guid CategoriaId { get; protected set; }
        public List<string> Imagens { get; set; }
        public List<Guid> Efeitos { get; set; }
        public InformacaoNutricionalInputModel InformacaoNutricional { get; protected set; }
    }

    public class InformacaoNutricionalInputModel
    {
        public string Cabecalho { get; set; }
        public string Legenda { get; set; }

        public List<CompostoNutricionalInputModel> CompostosNutricionais { get; set; }
    }

    public class CompostoNutricionalInputModel
    {
        public string Composto { get; set; }
        public string Porcao { get; set; }
        public string ValorDiario { get; set; }
    }
}
