using Suplee.Vendas.Domain.Commands;
using System;

namespace Suplee.Test.Builder.Commands.Vendas
{
    public class IniciarPedidoCommandBuilder : IniciarPedidoCommand
    {
        public IniciarPedidoCommandBuilder(Guid usuarioId = default, bool sucesso = default) : base(usuarioId, sucesso)
        {
        }

        public IniciarPedidoCommandBuilder ComandoValido()
        {
            UsuarioId = Guid.NewGuid();
            Sucesso = true;

            return this;
        }

        public IniciarPedidoCommandBuilder ComandoInvalido()
        {
            UsuarioId = Guid.Empty;

            return this;
        }

        public IniciarPedidoCommandBuilder ComUsuarioId(Guid usuarioId)
        {
            UsuarioId = usuarioId;

            return this;
        }

        public IniciarPedidoCommandBuilder ComFalha()
        {
            Sucesso = false;

            return this;
        }

        public IniciarPedidoCommand Build() => this;
    }
}
