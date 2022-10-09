using Suplee.Core.DomainObjects;
using Suplee.Identidade.Domain.Enums;
using System;

namespace Suplee.Identidade.Domain.Models
{
    public class Endereco : Entity
    {
        public Endereco() { }
        public Endereco(
            Guid usuarioId,
            string nomeDestinatario,
            string cep,
            string estado,
            string cidade,
            string bairro,
            string rua,
            string numero,
            string complemento,
            ETipoLocal tipoLocal,
            string telefone,
            string informacaoAdicional)
        {
            UsuarioId = usuarioId;
            NomeDestinatario = nomeDestinatario;
            CEP = cep;
            Estado = estado;
            Cidade = cidade;
            Bairro = bairro;
            Rua = rua;
            Numero = numero;
            Complemento = complemento;
            TipoLocal = tipoLocal;
            Telefone = telefone;
            InformacaoAdicional = informacaoAdicional;
        }

        public Guid UsuarioId { get; protected set; }
        public string NomeDestinatario { get; protected set; }
        public string CEP { get; protected set; }
        public string Estado { get; protected set; }
        public string Cidade { get; protected set; }
        public string Bairro { get; protected set; }
        public string Rua { get; protected set; }
        public string Numero { get; protected set; }
        public string Complemento { get; protected set; }
        public ETipoLocal TipoLocal { get; protected set; }
        public string Telefone { get; protected set; }
        public string InformacaoAdicional { get; protected set; }
        public bool EnderecoPadrao { get; protected set; }

        public Usuario Usuario { get; protected set; }

        public void VincularUsuarioId(Guid usuarioId) => UsuarioId = usuarioId;
    }
}
