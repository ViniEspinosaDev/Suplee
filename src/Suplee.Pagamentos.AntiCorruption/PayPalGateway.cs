using System;
using System.Linq;

namespace Suplee.Pagamentos.AntiCorruption
{
    public class PayPalGateway : IPayPalGateway
    {
        public bool CommitTransaction(bool sucesso, string cardHashKey = default, string orderId = default, decimal amount = default)
        {
            //return new Random().Next(2) == 0;
            return sucesso;
        }

        public string GetCardHashKey(string serviceKey, string cartaoCredito)
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public string GetPayPalServiceKey(string apiKey, string encriptionKey)
        {
            return new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}