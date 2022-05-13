using Suplee.Core.DomainObjects;
using System;

namespace Suplee.Catalogo.Domain.Models
{
    public class CompostoNutricional : Entity
    {
        public CompostoNutricional(Guid informacaoNutricionalId, string composto, string porcao, string valorDiario)
        {
            InformacaoNutricionalId = informacaoNutricionalId;
            Composto = composto;
            Porcao = porcao;
            ValorDiario = valorDiario;
        }

        public Guid InformacaoNutricionalId { get; protected set; }
        public string Composto { get; protected set; }
        public string Porcao { get; protected set; }
        public string ValorDiario { get; protected set; }
    }
}
