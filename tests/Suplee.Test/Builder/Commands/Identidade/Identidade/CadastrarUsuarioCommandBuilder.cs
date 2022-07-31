using Suplee.Core.Tools;
using Suplee.Identidade.Domain.Identidade.Commands;

namespace Suplee.Test.Builder.Commands.Identidade.Identidade
{
    public class CadastrarUsuarioCommandBuilder : CadastrarUsuarioCommand
    {
        public CadastrarUsuarioCommandBuilder(
            string nome = default,
            string email = default,
            string cpf = default,
            string senha = default,
            string confirmacaoSenha = default,
            string celular = default) : base(nome, email, cpf, senha, confirmacaoSenha, celular)
        {
        }

        public CadastrarUsuarioCommandBuilder ComandoValido()
        {
            Nome = "Cadastrar Usuario";
            Email = "cadastrar.usuario@email.com";
            CPF = "999.999.999-84".FormatarCPFApenasNumeros();
            Senha = "@Senha8080";
            ConfirmacaoSenha = "@Senha8080";
            Celular = "15997826699";

            return this;
        }

        public CadastrarUsuarioCommandBuilder ComandoInvalido()
        {
            Nome = "";
            Email = "";
            CPF = "";
            Senha = "";
            ConfirmacaoSenha = "";

            return this;
        }

        public CadastrarUsuarioCommandBuilder ComSenhas(string senha, string confirmacaoSenha)
        {
            Senha = senha;
            ConfirmacaoSenha = confirmacaoSenha;

            return this;
        }

        public CadastrarUsuarioCommand Build() => this;
    }
}
