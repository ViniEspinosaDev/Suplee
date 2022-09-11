﻿using FluentValidation;
using Suplee.Core.Messages;
using System;

namespace Suplee.Vendas.Domain.Commands
{
    public class CancelarProcessamentoPedidoCommand : Command<bool>
    {
        public CancelarProcessamentoPedidoCommand(Guid pedidoId, Guid usuarioId)
        {
            AggregateId = pedidoId;
            PedidoId = pedidoId;
            UsuarioId = usuarioId;
        }

        public Guid PedidoId { get; protected set; }
        public Guid UsuarioId { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new CancelarProcessamentoPedidoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CancelarProcessamentoPedidoCommandValidation : AbstractValidator<CancelarProcessamentoPedidoCommand>
    {
        public CancelarProcessamentoPedidoCommandValidation()
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