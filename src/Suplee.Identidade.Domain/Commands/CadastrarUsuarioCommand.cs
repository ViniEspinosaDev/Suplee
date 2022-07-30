using FluentValidation;
using Suplee.Core.Messages;
using Suplee.Identidade.Domain.Models;

namespace Suplee.Identidade.Domain.Commands
{
    public class CadastrarUsuarioCommand : Command<Usuario>
    {
        public CadastrarUsuarioCommand(string nome, string email, string cpf, string senha, string confirmacaoSenha, string celular)
        {
            Nome = nome;
            Email = email;
            CPF = cpf;
            Senha = senha;
            ConfirmacaoSenha = confirmacaoSenha;
            Celular = celular;
        }

        public string Nome { get; protected set; }
        public string Email { get; protected set; }
        public string CPF { get; protected set; }
        public string Celular { get; protected set; }
        public string Senha { get; protected set; }
        public string ConfirmacaoSenha { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarUsuarioCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CadastrarUsuarioCommandValidation : AbstractValidator<CadastrarUsuarioCommand>
    {
        public CadastrarUsuarioCommandValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome do usuário não foi informado");

            RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage("O E-mail do usuário não foi informado");

            RuleFor(c => c.CPF)
                .NotEmpty()
                .WithMessage("O CPF do usuário não foi informado");

            RuleFor(c => c.Senha)
                .NotEmpty()
                .WithMessage("A Senha do usuário não foi informado");

            RuleFor(c => c.ConfirmacaoSenha)
                .NotEmpty()
                .WithMessage("A Confirmação do usuário não foi informado");
        }
    }
}
