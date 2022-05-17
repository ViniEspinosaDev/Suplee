using Suplee.Core.DomainObjects;

namespace Suplee.Catalogo.Domain.Models
{
    public class Efeito : Entity
    {
        public Efeito(string nome, string descricao, string icone)
        {
            Nome = nome;
            Descricao = descricao;
            Icone = icone;
        }

        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        public string Icone { get; protected set; }

        public void Atualizar(string nome, string descricao, string icone)
        {
            Nome = nome;
            Descricao = descricao;
            Icone = icone;
        }
    }
}
