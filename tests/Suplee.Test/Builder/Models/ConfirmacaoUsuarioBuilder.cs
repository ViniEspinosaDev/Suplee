using Suplee.Core.Tools;
using Suplee.Identidade.Domain.Models;
using System;

namespace Suplee.Test.Builder.Models
{
    public class ConfirmacaoUsuarioBuilder : ConfirmacaoUsuario
    {
        public ConfirmacaoUsuarioBuilder(
            Guid usuarioId = default,
            string codigoConfirmacao = default) : base(usuarioId, codigoConfirmacao)
        {
        }

        public ConfirmacaoUsuarioBuilder PadraoValido()
        {
            UsuarioId = Guid.NewGuid();
            CodigoConfirmacao = HashPassword.GenerateRandomCode(10);

            return this;
        }

        public ConfirmacaoUsuario Build() => this;
    }
}
