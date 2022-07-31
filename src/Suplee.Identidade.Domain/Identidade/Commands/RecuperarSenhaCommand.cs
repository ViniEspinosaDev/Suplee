using FluentValidation;
using Suplee.Core.Messages;
using Suplee.Core.Tools;

namespace Suplee.Identidade.Domain.Identidade.Commands
{
    public class RecuperarSenhaCommand : Command<string>
    {
        public RecuperarSenhaCommand(string cpf)
        {
            CPF = cpf.FormatarCPFApenasNumeros();
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
               .NotEmpty().WithMessage("O CPF do usuário não foi informado")
               .MinimumLength(11).WithMessage("O CPF é inválido")
               .MaximumLength(11).WithMessage("O CPF é inválido")
               .OverridePropertyName("ValidacaoCPF");
        }
    }
}
