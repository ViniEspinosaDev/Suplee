using Suplee.Identidade.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Suplee.Api.Controllers.Identidade.ViewModels
{
    public class UsuarioViewModel
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Celular { get; set; }
        public ETipoUsuario Tipo { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<EnderecoViewModel> Enderecos { get; set; }
    }

    public class EnderecoViewModel
    {
        public Guid EnderecoId { get; set; }
        public string NomeDestinatario { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public ETipoLocal TipoLocal { get; set; }
        public string Telefone { get; set; }
        public string InformacaoAdicional { get; set; }
        public bool EnderecoPadrao { get; set; }
    }
}
