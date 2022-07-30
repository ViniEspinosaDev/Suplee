using FluentValidation;
using Suplee.Core.Messages;
using System;

namespace Suplee.Identidade.Domain.Autenticacao.Commands
{
    public class ConfirmarCadastroCommand : Command<bool>
    {
        public ConfirmarCadastroCommand(Guid usuarioId, string codigoConfirmacao)
        {
            UsuarioId = usuarioId;
            CodigoConfirmacao = codigoConfirmacao;
        }

        public Guid UsuarioId { get; protected set; }
        public string CodigoConfirmacao { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new ConfirmarCadastroCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ConfirmarCadastroCommandValidation : AbstractValidator<ConfirmarCadastroCommand>
    {
        public ConfirmarCadastroCommandValidation()
        {
            RuleFor(c => c.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O Id do usuário não foi informado");

            RuleFor(c => c.CodigoConfirmacao)
                .NotEmpty().WithMessage("O código de confirmação não foi informado")
                .MinimumLength(10).WithMessage("O código de confirmação deve ter 10 caracteres")
                .MaximumLength(10).WithMessage("O código de confirmação deve ter 10 caracteres");
        }
    }
}
