using FluentValidation;
using Suplee.Core.Messages;
using System;

namespace Suplee.Identidade.Domain.Identidade.Commands
{
    public class MarcarEnderecoComoPadraoCommand : Command<bool>
    {
        public MarcarEnderecoComoPadraoCommand(Guid usuarioId, Guid enderecoId)
        {
            UsuarioId = usuarioId;
            EnderecoId = enderecoId;
        }

        public Guid UsuarioId { get; protected set; }
        public Guid EnderecoId { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new MarcarEnderecoComoPadraoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class MarcarEnderecoComoPadraoCommandValidation : AbstractValidator<MarcarEnderecoComoPadraoCommand>
    {
        public MarcarEnderecoComoPadraoCommandValidation()
        {
            RuleFor(x => x.UsuarioId)
               .NotEqual(Guid.Empty).WithMessage("O Id do usuário não foi informado")
               .OverridePropertyName("ValidacaoUsuarioId");

            RuleFor(x => x.UsuarioId)
               .NotEqual(Guid.Empty).WithMessage("O Id do endereço não foi informado")
               .OverridePropertyName("ValidacaoEnderecoId");
        }
    }
}
