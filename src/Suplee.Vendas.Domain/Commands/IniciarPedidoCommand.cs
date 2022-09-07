using FluentValidation;
using Suplee.Core.Messages;
using System;

namespace Suplee.Vendas.Domain.Commands
{
    public class IniciarPedidoCommand : Command<bool>
    {
        public IniciarPedidoCommand(Guid usuarioId, bool sucesso)
        {
            UsuarioId = usuarioId;
            Sucesso = sucesso;
        }

        public Guid UsuarioId { get; protected set; }
        public bool Sucesso { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new PagarPedidoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class PagarPedidoCommandValidation : AbstractValidator<IniciarPedidoCommand>
    {
        public PagarPedidoCommandValidation()
        {
            RuleFor(c => c.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O Id do usuário não foi informado")
                .OverridePropertyName("ValidacaoUsuarioId");
        }
    }
}
