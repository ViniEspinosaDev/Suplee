using Suplee.Core.Tools;
using Suplee.Identidade.Domain.Enums;
using Suplee.Identidade.Domain.Models;
using System.Collections.Generic;

namespace Suplee.Test.Builder.Models
{
    public class UsuarioBuilder : Usuario
    {
        public UsuarioBuilder(
            string nome = default,
            string email = default,
            string senha = default,
            string cPF = default,
            string celular = default,
            ETipoUsuario tipo = default,
            EStatusUsuario status = default) : base(nome, email, senha, cPF, celular, tipo, status)
        {
        }

        public UsuarioBuilder PadraoValido()
        {
            Nome = "Usuário Padrão Válido";
            Email = "usuario.padrao@email.com";
            Senha = HashPassword.GenerateSHA512String("@Senha8080");
            CPF = "999.999.999-99".FormatarCPFApenasNumeros();
            Celular = "15997826699";
            Tipo = ETipoUsuario.Normal;
            Status = EStatusUsuario.AguardandoConfirmacao;
            Enderecos = new List<Endereco>() { new EnderecoBuilder().PadraoValido().ComUsuario(this).Build() };

            return this;
        }

        public Usuario Build() => this;
    }
}
