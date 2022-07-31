using FluentValidation;
using Suplee.Core.Messages;
using Suplee.Identidade.Domain.Models;

namespace Suplee.Identidade.Domain.Autenticacao.Commands
{
    public class RealizarLoginEmailCommand : Command<Usuario>
    {
        public RealizarLoginEmailCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public string Email { get; protected set; }
        public string Senha { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new RealizarLoginEmailCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RealizarLoginEmailCommandValidation : AbstractValidator<RealizarLoginEmailCommand>
    {
        public RealizarLoginEmailCommandValidation()
        {
            Validar();
        }

        private void Validar()
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("O E-mail não foi informado")
                .OverridePropertyName("ValidacaoEmail");

            RuleFor(r => r.Senha)
                .NotEmpty().WithMessage("A Senha não foi informada")
                .OverridePropertyName("ValidacaoSenha");
        }
    }
}
