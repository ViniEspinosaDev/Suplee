using Suplee.Core.Tools;
using Suplee.Identidade.Domain.Identidade.Commands;

namespace Suplee.Test.Builder.Commands.Identidade.Identidade
{
    public class ReenviarEmailConfirmarCadastroCommandBuilder : ReenviarEmailConfirmarCadastroCommand
    {
        public ReenviarEmailConfirmarCadastroCommandBuilder(string cpf = default) : base(cpf)
        {
        }

        public ReenviarEmailConfirmarCadastroCommandBuilder ComandoValido()
        {
            CPF = "999.999.999-99".FormatarCPFApenasNumeros();

            return this;
        }

        public ReenviarEmailConfirmarCadastroCommandBuilder ComandoInvalido()
        {
            CPF = "";

            return this;
        }

        public ReenviarEmailConfirmarCadastroCommand Build() => this;
    }
}
