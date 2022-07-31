using Suplee.Identidade.Domain.Autenticacao.Commands;

namespace Suplee.Test.Builder.Commands.Identidade.Autenticacao
{
    public class RealizarLoginEmailCommandBuilder : RealizarLoginEmailCommand
    {
        public RealizarLoginEmailCommandBuilder(string email = default, string senha = default) : base(email, senha)
        {
        }

        public RealizarLoginEmailCommandBuilder ComandoValido()
        {
            Email = "comando.valido@email.com";
            Senha = "@Senha8080";

            return this;
        }

        public RealizarLoginEmailCommandBuilder ComandoInvalido()
        {
            Email = "";
            Senha = "";

            return this;
        }

        public RealizarLoginEmailCommand Build() => this;
    }
}
