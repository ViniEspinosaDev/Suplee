using Suplee.Core.Tools;
using Suplee.Identidade.Domain.Autenticacao.Commands;

namespace Suplee.Test.Builder.Commands.Identidade.Autenticacao
{
    public class RealizarLoginCPFCommandBuilder : RealizarLoginCPFCommand
    {
        public RealizarLoginCPFCommandBuilder(string cpf = default, string senha = default) : base(cpf, senha)
        {
        }

        public RealizarLoginCPFCommandBuilder ComandoValido()
        {
            CPF = "999.999.999-99".FormatarCPFApenasNumeros();
            Senha = "@Senha8080";

            return this;
        }

        public RealizarLoginCPFCommandBuilder ComandoInvalido()
        {
            CPF = "";
            Senha = "";

            return this;
        }

        public RealizarLoginCPFCommand Build() => this;
    }
}
