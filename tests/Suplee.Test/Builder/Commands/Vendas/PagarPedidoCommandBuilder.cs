using Suplee.Vendas.Domain.Commands;
using System;

namespace Suplee.Test.Builder.Commands.Vendas
{
    public class PagarPedidoCommandBuilder : IniciarPedidoCommand
    {
        public PagarPedidoCommandBuilder(Guid usuarioId = default, bool sucesso = default) : base(usuarioId, sucesso)
        {
        }

        public PagarPedidoCommandBuilder ComandoValido()
        {
            UsuarioId = Guid.NewGuid();
            Sucesso = true;

            return this;
        }

        public PagarPedidoCommandBuilder ComandoInvalido()
        {
            UsuarioId = Guid.Empty;

            return this;
        }

        public PagarPedidoCommandBuilder ComUsuarioId(Guid usuarioId)
        {
            UsuarioId = usuarioId;

            return this;
        }

        public PagarPedidoCommandBuilder ComFalha()
        {
            Sucesso = false;

            return this;
        }

        public IniciarPedidoCommand Build() => this;
    }
}
