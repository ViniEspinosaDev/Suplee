using System;
using System.Security.Cryptography;
using System.Text;

namespace Suplee.Identidade.Domain.Tools
{
    public static class HashPassword
    {
        public static string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        public static string GerarCodigoConfirmacao()
        {
            var caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var codigoConfirmacao = new char[10];
            var aleatorio = new Random();

            for (int i = 0; i < codigoConfirmacao.Length; i++)
            {
                codigoConfirmacao[i] = caracteres[aleatorio.Next(caracteres.Length)];
            }

            return new String(codigoConfirmacao);
        }
    }
}
