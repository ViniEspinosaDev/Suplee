using Suplee.Vendas.Domain.Commands;
using System;

namespace Suplee.Test.Builder.Commands.Vendas
{
    public class CancelarProcessamentoPedidoCommandBuilder : CancelarProcessamentoPedidoCommand
    {
        public CancelarProcessamentoPedidoCommandBuilder(
            Guid pedidoId = default,
            Guid usuarioId = default) : base(pedidoId, usuarioId)
        {
        }

        public CancelarProcessamentoPedidoCommandBuilder ComandoValido()
        {
            PedidoId = Guid.NewGuid();
            UsuarioId = Guid.NewGuid();

            return this;
        }

        public CancelarProcessamentoPedidoCommandBuilder ComandoInvalido()
        {
            PedidoId = Guid.Empty;
            UsuarioId = Guid.Empty;

            return this;
        }

        public CancelarProcessamentoPedidoCommandBuilder ComUsuarioId(Guid usuarioId)
        {
            UsuarioId = usuarioId;

            return this;
        }

        public CancelarProcessamentoPedidoCommandBuilder ComPedidoId(Guid pedidoId)
        {
            PedidoId = pedidoId;

            return this;
        }

        public CancelarProcessamentoPedidoCommand Build() => this;
    }
}
