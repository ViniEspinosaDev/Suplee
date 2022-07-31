using Suplee.Identidade.Domain.Enums;
using System.Collections.Generic;

namespace Suplee.Api.Controllers.Identidade.InputModels
{
    /// <summary>
    /// Editar usuário
    /// </summary>
    public class EditarUsuarioInputModel
    {
        /// <summary>
        /// Id usuário
        /// </summary>
        public string UsuarioId { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Celular
        /// </summary>
        public string Celular { get; set; }
        /// <summary>
        /// Endereços
        /// </summary>
        public List<EnderecoInputModel> Enderecos { get; set; }

        /// <summary>
        /// Método para mapear usuário id nos endereços
        /// </summary>
        public void AdicionarUsuarioIdNosEnderecos() => Enderecos?.ForEach(x => x.UsuarioId = UsuarioId);
    }

    /// <summary>
    /// Endereço
    /// </summary>
    public class EnderecoInputModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Id do usuário
        /// </summary>
        public string UsuarioId { get; set; }
        /// <summary>
        /// Nome de quem vai receber
        /// </summary>
        public string NomeDestinatario { get; set; }
        /// <summary>
        /// CEP
        /// </summary>
        public string CEP { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        public string Estado { get; set; }
        /// <summary>
        /// Cidade
        /// </summary>
        public string Cidade { get; set; }
        /// <summary>
        /// Bairro
        /// </summary>
        public string Bairro { get; set; }
        /// <summary>
        /// Rua
        /// </summary>
        public string Rua { get; set; }
        /// <summary>
        /// Número da casa
        /// </summary>
        public string Numero { get; set; }
        /// <summary>
        /// Complemento
        /// </summary>
        public string Complemento { get; set; }
        /// <summary>
        /// Tipo (Casa = 0, Trabalho = 1)
        /// </summary>
        public ETipoLocal TipoLocal { get; set; }
        /// <summary>
        /// Telefone
        /// </summary>
        public string Telefone { get; set; }
        /// <summary>
        /// Informação adicional
        /// </summary>
        public string InformacaoAdicional { get; set; }
    }
}
