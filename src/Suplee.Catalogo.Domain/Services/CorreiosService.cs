using Suplee.Catalogo.Domain.DTO;
using Suplee.Catalogo.Domain.Interfaces.Services;
using System;

namespace Suplee.Catalogo.Domain.Services
{
    public class CorreiosService : ICorreiosService
    {
        public FreteDTO CalcularFrete(Guid produtoId, string cep)
        {
            Random aleatorio = new Random();

            int dias = aleatorio.Next(7, 31);
            int dezenas = aleatorio.Next(16, 72);
            int centavos = aleatorio.Next(10, 99);

            string valorTexto = $"{dezenas},{centavos}";

            decimal.TryParse(valorTexto, out decimal valor);

            return new FreteDTO()
            {
                Preco = valor,
                PrazoDias = dias,
                DataEstimada = DateTime.Now.AddDays(dias)
            };
        }
    }
}
