using FluentValidation;
using Suplee.Core.Messages;
using Suplee.Identidade.Domain.Models;

namespace Suplee.Identidade.Domain.Autenticacao.Commands
{
    public class RealizarLoginCPFCommand : Command<Usuario>
    {
        public RealizarLoginCPFCommand(string cpf, string senha)
        {
            CPF = cpf;
            Senha = senha;
        }

        public string CPF { get; protected set; }
        public string Senha { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new RealizarLoginCPFCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RealizarLoginCPFCommandValidation : AbstractValidator<RealizarLoginCPFCommand>
    {
        public RealizarLoginCPFCommandValidation()
        {
            Validar();
        }

        private void Validar()
        {
            RuleFor(r => r.CPF)
                .NotEmpty().WithMessage("O CPF não foi informado");

            RuleFor(r => r.Senha)
                .NotEmpty().WithMessage("A Senha não foi informada");
        }
    }
}
