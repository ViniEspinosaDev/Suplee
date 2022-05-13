using Suplee.Catalogo.Domain.Enums;
using Suplee.Core.DomainObjects;

namespace Suplee.Catalogo.Domain.Models
{
    public class Categoria : Entity
    {
        public Categoria(string nome, string descricao, string icone, ECor cor)
        {
            Nome = nome;
            Descricao = descricao;
            Icone = icone;
            Cor = cor;
        }

        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        public string Icone { get; protected set; }
        public ECor Cor { get; protected set; }
    }
}
