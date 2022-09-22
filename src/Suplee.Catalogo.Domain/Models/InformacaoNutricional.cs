using Suplee.Core.DomainObjects;
using System.Collections.Generic;
using System.Linq;

namespace Suplee.Catalogo.Domain.Models
{
    public class InformacaoNutricional : Entity
    {
        public InformacaoNutricional() { }
        public InformacaoNutricional(string cabecalho, string legenda, List<CompostoNutricional> compostosNutricionais = null)
        {
            Cabecalho = cabecalho;
            Legenda = legenda;
            CompostosNutricionais = compostosNutricionais ?? new List<CompostoNutricional>();
        }

        public string Cabecalho { get; protected set; }
        public string Legenda { get; protected set; }

        public ICollection<CompostoNutricional> CompostosNutricionais { get; protected set; }

        public void MapearCompostosNutricionais()
        {
            if (CompostosNutricionais is null) return;

            int quantidade = 0;

            CompostosNutricionais.ToList().ForEach(x => x.MapearIdPaiEOrdem(Id, quantidade++));
        }
    }
}
