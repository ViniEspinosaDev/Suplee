using FluentValidation;
using Suplee.Core.Messages;
using Suplee.Core.Tools;
using Suplee.Identidade.Domain.Models;

namespace Suplee.Identidade.Domain.Identidade.Commands
{
    public class CadastrarUsuarioCommand : Command<Usuario>
    {
        public CadastrarUsuarioCommand(string nome, string email, string cpf, string senha, string confirmacaoSenha, string celular)
        {
            Nome = nome;
            Email = email;
            CPF = cpf.FormatarCPFApenasNumeros();
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
                .NotEmpty().WithMessage("O nome do usuário não foi informado")
                .MinimumLength(3).WithMessage("O nome do usuário deve ter ao mínimo 3 caracteres")
                .OverridePropertyName("ValidacaoNome");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O E-mail do usuário não foi informado")
                .EmailAddress().WithMessage("O E-mail não é válido")
                .OverridePropertyName("ValidacaoEmail");

            RuleFor(c => c.CPF)
                .NotEmpty().WithMessage("O CPF do usuário não foi informado")
                .MinimumLength(11).WithMessage("O CPF é inválido")
                .MaximumLength(11).WithMessage("O CPF é inválido")
                .OverridePropertyName("ValidacaoCPF");

            RuleFor(c => c.Senha)
                .NotEmpty().WithMessage("A Senha do usuário não foi informada")
                .MinimumLength(6).WithMessage("A senha deve ter ao mínimo 6 caracteres")
                .OverridePropertyName("ValidacaoSenha");

            RuleFor(c => c.ConfirmacaoSenha)
                .NotEmpty().WithMessage("A Confirmação de senha do usuário não foi informada")
                .MinimumLength(6).WithMessage("A Confirmação de senha deve ter ao mínimo 6 caracteres")
                .OverridePropertyName("ValidacaoConfirmacaoSenha");
        }
    }
}
