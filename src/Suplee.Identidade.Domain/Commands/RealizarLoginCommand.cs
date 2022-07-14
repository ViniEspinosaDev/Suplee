using FluentValidation;
using Suplee.Core.Messages;

namespace Suplee.Identidade.Domain.Commands
{
    public class RealizarLoginCommand : Command
    {
        public RealizarLoginCommand(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public string Email { get; protected set; }
        public string Senha { get; protected set; }

        public override bool IsValid()
        {
            var validacao = new RealizarLoginCommandValidation().Validate(this);
            return validacao.IsValid;
        }
    }

    public class RealizarLoginCommandValidation : AbstractValidator<RealizarLoginCommand>
    {
        public RealizarLoginCommandValidation()
        {
            Validar();
        }

        private void Validar()
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("O E-mail não foi informado");

            RuleFor(r => r.Senha)
                .NotEmpty().WithMessage("A Senha não foi informada");
        }
    }
}
