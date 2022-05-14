using Suplee.Core.DomainObjects;
using System.Collections.Generic;

namespace Suplee.Catalogo.Domain.Models
{
    public class InformacaoNutricional : Entity
    {
        protected InformacaoNutricional()
        {

        }

        public InformacaoNutricional(string cabecalho, string legenda, List<CompostoNutricional> compostosNutricionais = null)
        {
            Cabecalho = cabecalho;
            Legenda = legenda;
            CompostosNutricionais = compostosNutricionais ?? new List<CompostoNutricional>();
        }

        public string Cabecalho { get; protected set; }
        public string Legenda { get; protected set; }

        public ICollection<CompostoNutricional> CompostosNutricionais { get; protected set; }
    }
}
