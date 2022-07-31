using System.Linq;

namespace Suplee.Core.Tools
{
    public static class TextExtensions
    {
        public static string FormatarCPFApenasNumeros(this string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return string.Empty;

            return new string(cpf.Where(x => x >= '0' && x <= '9').ToArray());
        }
    }
}
