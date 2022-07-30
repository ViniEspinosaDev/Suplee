using FluentValidation;
using Suplee.Core.Messages;
using System;

namespace Suplee.Identidade.Domain.Identidade.Commands
{
    public class AlterarSenhaCommand : Command<bool>
    {
        public AlterarSenhaCommand(Guid usuarioId, string codigoConfirmacao, string senha, string confirmacaoSenha)
        {
            UsuarioId = usuarioId;
            CodigoConfirmacao = codigoConfirmacao;
            Senha = senha;
            ConfirmacaoSenha = confirmacaoSenha;
        }

        public Guid UsuarioId { get; protected set; }
        public string CodigoConfirmacao { get; protected set; }
        public string Senha { get; protected set; }
        public string ConfirmacaoSenha { get; protected set; }
        public override bool IsValid()
        {
            ValidationResult = new AlterarSenhaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AlterarSenhaCommandValidation : AbstractValidator<AlterarSenhaCommand>
    {
        public AlterarSenhaCommandValidation()
        {
            RuleFor(c => c.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O Id do usuário não foi informado");

            RuleFor(c => c.CodigoConfirmacao)
                .NotEmpty()
                .WithMessage("O Código de confirmação não foi informado");

            RuleFor(c => c.Senha)
                .NotEmpty()
                .WithMessage("A Senha do usuário não foi informada")
                .MinimumLength(6).WithMessage("A senha deve ter ao mínimo 6 caracteres");

            RuleFor(c => c.ConfirmacaoSenha)
                .NotEmpty()
                .WithMessage("A Confirmação de senha do usuário não foi informada")
                .MinimumLength(6).WithMessage("A Confirmação de senha deve ter ao mínimo 6 caracteres");
        }
    }
}
