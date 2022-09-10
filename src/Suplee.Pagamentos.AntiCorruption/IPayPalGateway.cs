namespace Suplee.Pagamentos.AntiCorruption
{
    public interface IPayPalGateway
    {
        string GetPayPalServiceKey(string apiKey, string encriptionKey);
        string GetCardHashKey(string serviceKey, string cartaoCredito);
        bool CommitTransaction(bool sucesso, string cardHashKey = default, string orderId = default, decimal amount = default);
    }
}
