using Suplee.Core.Tools;
using Suplee.Identidade.Domain.Identidade.Commands;

namespace Suplee.Test.Builder.Commands.Identidade.Identidade
{
    public class RecuperarSenhaCommandBuilder : RecuperarSenhaCommand
    {
        public RecuperarSenhaCommandBuilder(string cpf = default) : base(cpf)
        {
        }

        public RecuperarSenhaCommandBuilder ComandoValido()
        {
            CPF = "999.999.999-99".FormatarCPFApenasNumeros();

            return this;
        }

        public RecuperarSenhaCommandBuilder ComandoInvalido()
        {
            CPF = "";

            return this;
        }

        public RecuperarSenhaCommand Build() => this;
    }
}
