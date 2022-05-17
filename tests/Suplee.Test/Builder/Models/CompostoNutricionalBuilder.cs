using Suplee.Catalogo.Domain.Models;

namespace Suplee.Test.Builder.Models
{
    public class CompostoNutricionalBuilder : CompostoNutricional
    {
        public CompostoNutricionalBuilder PadraoValido()
        {
            Composto = "Composto";
            Porcao = "Porcao";
            ValorDiario = "Valor Diário";
            Ordem = 1;

            return this;
        }

        public CompostoNutricional Build() => this;
    }
}
