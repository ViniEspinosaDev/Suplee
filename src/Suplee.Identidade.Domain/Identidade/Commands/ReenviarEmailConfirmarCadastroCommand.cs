using FluentValidation;
using Suplee.Core.Messages;
using Suplee.Core.Tools;

namespace Suplee.Identidade.Domain.Identidade.Commands
{
    public class ReenviarEmailConfirmarCadastroCommand : Command<string>
    {
        public ReenviarEmailConfirmarCadastroCommand(string cpf)
        {
            CPF = cpf.FormatarCPFApenasNumeros();
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
               .NotEmpty().WithMessage("O CPF do usuário não foi informado")
               .MinimumLength(11).WithMessage("O CPF é inválido")
               .MaximumLength(11).WithMessage("O CPF é inválido");
        }
    }
}
