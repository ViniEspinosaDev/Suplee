using Suplee.Core.DomainObjects;
using System;

namespace Suplee.Catalogo.Domain.Models
{
    public class CompostoNutricional : Entity
    {
        protected CompostoNutricional()
        {

        }

        public CompostoNutricional(Guid informacaoNutricionalId, string composto, string porcao, string valorDiario, int ordem)
        {
            InformacaoNutricionalId = informacaoNutricionalId;
            Composto = composto;
            Porcao = porcao;
            ValorDiario = valorDiario;
            Ordem = ordem;
        }

        public Guid InformacaoNutricionalId { get; protected set; }
        public string Composto { get; protected set; }
        public string Porcao { get; protected set; }
        public string ValorDiario { get; protected set; }
        public int Ordem { get; protected set; }

        public InformacaoNutricional InformacaoNutricional { get; protected set; }

        public void MapearIdPaiEOrdem(Guid informacaoNutricionalId, int ordem)
        {
            InformacaoNutricionalId = informacaoNutricionalId;
            Ordem = ordem;
        }
    }
}
