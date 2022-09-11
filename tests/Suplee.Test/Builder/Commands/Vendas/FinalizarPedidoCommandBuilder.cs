using Suplee.Vendas.Domain.Commands;
using System;

namespace Suplee.Test.Builder.Commands.Vendas
{
    public class FinalizarPedidoCommandBuilder : FinalizarPedidoCommand
    {
        public FinalizarPedidoCommandBuilder(Guid pedidoId = default, Guid usuarioId = default) : base(pedidoId, usuarioId)
        {
        }

        public FinalizarPedidoCommandBuilder ComandoValido()
        {
            PedidoId = Guid.NewGuid();
            UsuarioId = Guid.NewGuid();

            return this;
        }

        public FinalizarPedidoCommandBuilder ComandoInvalido()
        {
            PedidoId = Guid.Empty;
            UsuarioId = Guid.Empty;

            return this;
        }

        public FinalizarPedidoCommandBuilder ComUsuarioId(Guid usuarioId)
        {
            UsuarioId = usuarioId;

            return this;
        }

        public FinalizarPedidoCommandBuilder ComPedidoId(Guid pedidoId)
        {
            PedidoId = pedidoId;

            return this;
        }

        public FinalizarPedidoCommand Build() => this;
    }
}
