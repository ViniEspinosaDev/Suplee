using FluentValidation;
using Suplee.Core.Messages;

namespace Suplee.Identidade.Domain.Identidade.Commands
{
    public class RecuperarSenhaCommand : Command<string>
    {
        public RecuperarSenhaCommand(string cpf)
        {
            CPF = cpf;
        }

        public string CPF { get; protected set; }
        public override bool IsValid()
        {
            ValidationResult = new RecuperarSenhaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RecuperarSenhaCommandValidation : AbstractValidator<RecuperarSenhaCommand>
    {
        public RecuperarSenhaCommandValidation()
        {
            RuleFor(c => c.CPF)
                .NotEmpty()
                .WithMessage("O CPF do usuário não foi informado");
        }
    }
}
