using Suplee.Core.Tools;
using Suplee.Identidade.Domain.Identidade.Commands;
using System;

namespace Suplee.Test.Builder.Commands.Identidade.Identidade
{
    public class AlterarSenhaCommandBuilder : AlterarSenhaCommand
    {
        public AlterarSenhaCommandBuilder(
            Guid usuarioId = default,
            string codigoConfirmacao = default,
            string senha = default,
            string confirmacaoSenha = default) : base(usuarioId, codigoConfirmacao, senha, confirmacaoSenha)
        {
        }

        public AlterarSenhaCommandBuilder ComandoValido()
        {
            UsuarioId = Guid.NewGuid();
            CodigoConfirmacao = HashPassword.GenerateRandomCode(10);
            Senha = "@Senha8080";
            ConfirmacaoSenha = "@Senha8080";

            return this;
        }

        public AlterarSenhaCommandBuilder ComandoInvalido()
        {
            UsuarioId = Guid.Empty;
            CodigoConfirmacao = "";
            Senha = "";
            ConfirmacaoSenha = "";

            return this;
        }

        public AlterarSenhaCommandBuilder ComSenhas(string senha, string confirmacaoSenha)
        {
            Senha = senha;
            ConfirmacaoSenha = confirmacaoSenha;

            return this;
        }

        public AlterarSenhaCommand Build() => this;

        //RuleFor(c => c.UsuarioId)
        //        .NotEqual(Guid.Empty).WithMessage("O Id do usuário não foi informado");

        //RuleFor(c => c.CodigoConfirmacao)
        //        .NotEmpty()
        //        .WithMessage("O Código de confirmação não foi informado");

        //RuleFor(c => c.Senha)
        //        .NotEmpty()
        //        .WithMessage("A Senha do usuário não foi informada")
        //        .MinimumLength(6).WithMessage("A senha deve ter ao mínimo 6 caracteres");

        //RuleFor(c => c.ConfirmacaoSenha)
        //        .NotEmpty()
        //        .WithMessage("A Confirmação de senha do usuário não foi informada")
        //        .MinimumLength(6).WithMessage("A Confirmação de senha deve ter ao mínimo 6 caracteres");
    }
}
