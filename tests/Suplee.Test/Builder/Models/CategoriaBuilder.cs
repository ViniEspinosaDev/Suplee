using Suplee.Catalogo.Domain.Enums;
using Suplee.Catalogo.Domain.Models;

namespace Suplee.Test.Builder.Models
{
    public class CategoriaBuilder : Categoria
    {
        public CategoriaBuilder(
            string nome = default,
            string descricao = default,
            string icone = default,
            ECor cor = default) : base(nome, descricao, icone, cor)
        {
        }

        public CategoriaBuilder PadraoValido()
        {
            Nome = "Nome";
            Descricao = "Descricao";
            Icone = "Icone";
            Cor = ECor.GreenNormal;

            return this;
        }

        public Categoria Build() => this;
    }
}
