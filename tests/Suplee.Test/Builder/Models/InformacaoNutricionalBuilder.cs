using Suplee.Catalogo.Domain.Models;
using System.Collections.Generic;

namespace Suplee.Test.Builder.Models
{
    public class InformacaoNutricionalBuilder : InformacaoNutricional
    {
        public InformacaoNutricionalBuilder PadraoValido()
        {
            Cabecalho = "Cabecalho";
            Legenda = "Legenda";

            return this;
        }

        public InformacaoNutricionalBuilder ComCompostosNutricionais(List<CompostoNutricional> compostosNutricionais = null)
        {
            if (compostosNutricionais == null)
                compostosNutricionais = new List<CompostoNutricional>() { new CompostoNutricionalBuilder().PadraoValido().Build() };

            CompostosNutricionais = compostosNutricionais;

            return this;
        }

        public InformacaoNutricional Build() => this;
    }
}
