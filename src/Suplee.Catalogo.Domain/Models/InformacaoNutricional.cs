using Suplee.Core.DomainObjects;
using System.Collections.Generic;

namespace Suplee.Catalogo.Domain.Models
{
    public class InformacaoNutricional : Entity
    {
        public InformacaoNutricional(string cabecalho, string legenda)
        {
            Cabecalho = cabecalho;
            Legenda = legenda;
        }

        public string Cabecalho { get; protected set; }
        public string Legenda { get; protected set; }

        public ICollection<CompostoNutricional> CompostosNutricionais { get; protected set; }
    }
}
