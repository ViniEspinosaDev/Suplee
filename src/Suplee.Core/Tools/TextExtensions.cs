using System.Linq;

namespace Suplee.Core.Tools
{
    public static class TextExtensions
    {
        public static string FormatarCPFApenasNumeros(this string cpf) => RecuperarApenasNumeros(cpf);

        public static string FormatarCEPApenasNumeros(this string cep) => RecuperarApenasNumeros(cep);

        private static string RecuperarApenasNumeros(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return string.Empty;

            return new string(texto.Where(x => x >= '0' && x <= '9').ToArray());
        }
    }
}
