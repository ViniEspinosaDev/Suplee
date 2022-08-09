using Suplee.Catalogo.Domain.Models;
using System;

namespace Suplee.Test.Builder.Models
{
    public class EfeitoBuilder : Efeito
    {
        public EfeitoBuilder(
            string nome = default,
            string descricao = default,
            string icone = default) : base(nome, descricao, icone)
        {
        }

        public EfeitoBuilder PadraoValido()
        {
            Nome = "Nome";
            Descricao = "Descrição";
            Icone = "Icone";

            return this;
        }

        public EfeitoBuilder ComNome(string nome)
        {
            Nome = nome;

            return this;
        }

        public Efeito Build() => this;
    }
}
