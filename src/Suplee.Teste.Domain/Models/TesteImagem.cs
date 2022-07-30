using Suplee.Core.DomainObjects;

namespace Suplee.Teste.Domain.Models
{
    public class TesteImagem : Entity, IAggregateRoot
    {
        public TesteImagem(string nomeMaximizada, string nomeReduzida, string urlMaximizada, string urlReduzida)
        {
            NomeMaximizada = nomeMaximizada;
            NomeReduzida = nomeReduzida;
            UrlMaximizada = urlMaximizada;
            UrlReduzida = urlReduzida;
        }

        public string NomeMaximizada { get; protected set; }
        public string NomeReduzida { get; protected set; }
        public string UrlMaximizada { get; protected set; }
        public string UrlReduzida { get; protected set; }
    }
}
