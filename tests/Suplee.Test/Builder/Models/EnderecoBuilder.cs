using Suplee.Core.Tools;
using Suplee.Identidade.Domain.Enums;
using Suplee.Identidade.Domain.Models;
using System;

namespace Suplee.Test.Builder.Models
{
    public class EnderecoBuilder : Endereco
    {
        public EnderecoBuilder PadraoValido()
        {
            UsuarioId = Guid.NewGuid();
            NomeDestinatario = "Nome Destinatário";
            CEP = "180160-85".FormatarCEPApenasNumeros();
            Estado = "ES";
            Cidade = "Cidade";
            Bairro = "Bairro";
            Rua = "Rua";
            Numero = "10";
            Complemento = "";
            TipoLocal = ETipoLocal.Casa;
            Telefone = "15997826699";
            InformacaoAdicional = "";

            return this;
        }

        public EnderecoBuilder PadraoInvalido()
        {
            UsuarioId = Guid.Empty;
            NomeDestinatario = "";
            CEP = "";
            Estado = "";
            Cidade = "";
            Bairro = "";
            Rua = "";
            Numero = "";
            Complemento = "";
            TipoLocal = default(ETipoLocal);
            Telefone = "";
            InformacaoAdicional = "";

            return this;
        }

        public EnderecoBuilder ComUsuario(Usuario usuario)
        {
            UsuarioId = usuario.Id;
            Usuario = usuario;

            return this;
        }

        public Endereco Build() => this;
    }
}
