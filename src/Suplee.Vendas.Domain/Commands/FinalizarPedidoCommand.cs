using FluentValidation;
using Suplee.Core.Messages;
using System;

namespace Suplee.Vendas.Domain.Commands
{
    public class FinalizarPedidoCommand : Command<bool>
    {
        public FinalizarPedidoCommand(Guid pedidoId, Guid usuarioId)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            UsuarioId = usuarioId;
        }

        public Guid PedidoId { get; protected set; }
        public Guid UsuarioId { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new FinalizarPedidoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class FinalizarPedidoCommandValidation : AbstractValidator<FinalizarPedidoCommand>
    {
        public FinalizarPedidoCommandValidation()
        {
            RuleFor(c => c.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O Id do usuário não foi informado")
                .OverridePropertyName("ValidacaoUsuarioId");

            RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty).WithMessage("O Id do pedido não foi informado")
                .OverridePropertyName("ValidacaoPedidoId");
        }
    }
}