using Suplee.Core.Tools;
using Suplee.Identidade.Domain.Autenticacao.Commands;
using System;

namespace Suplee.Test.Builder.Commands.Identidade.Autenticacao
{
    public class ConfirmarCadastroCommandBuilder : ConfirmarCadastroCommand
    {
        public ConfirmarCadastroCommandBuilder(Guid usuarioId = default, string codigoConfirmacao = default) : base(usuarioId, codigoConfirmacao)
        {
        }

        public ConfirmarCadastroCommandBuilder ComandoValido()
        {
            UsuarioId = Guid.NewGuid();
            CodigoConfirmacao = HashPassword.GenerateRandomCode(10);

            return this;
        }

        public ConfirmarCadastroCommandBuilder ComandoInvalido()
        {
            UsuarioId = Guid.Empty;
            CodigoConfirmacao = "";

            return this;
        }

        public ConfirmarCadastroCommand Build() => this;
    }
}
