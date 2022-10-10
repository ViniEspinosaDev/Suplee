using FluentValidation;
using Suplee.Core.Messages;
using System;

namespace Suplee.Vendas.Domain.Commands
{
    public class IniciarPedidoCommand : Command<bool>
    {
        public IniciarPedidoCommand(bool sucesso)
        {
            Sucesso = sucesso;
        }

        public Guid UsuarioId { get; protected set; }
        public bool Sucesso { get; protected set; }

        public void VincularUsuarioId(Guid usuarioId) => UsuarioId = usuarioId;

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
