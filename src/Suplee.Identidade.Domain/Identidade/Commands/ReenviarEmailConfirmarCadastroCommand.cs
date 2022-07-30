using FluentValidation;
using Suplee.Core.Messages;

namespace Suplee.Identidade.Domain.Identidade.Commands
{
    public class ReenviarEmailConfirmarCadastroCommand : Command<string>
    {
        public ReenviarEmailConfirmarCadastroCommand(string cpf)
        {
            CPF = cpf;
        }

        public string CPF { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new ReenviarEmailConfirmarCadastroCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ReenviarEmailConfirmarCadastroCommandValidation : AbstractValidator<ReenviarEmailConfirmarCadastroCommand>
    {
        public ReenviarEmailConfirmarCadastroCommandValidation()
        {
            RuleFor(c => c.CPF)
                .NotEmpty()
                .WithMessage("O CPF do usuário não foi informado");
        }
    }
}
