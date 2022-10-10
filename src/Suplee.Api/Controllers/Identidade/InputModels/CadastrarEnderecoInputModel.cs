using Suplee.Identidade.Domain.Enums;

namespace Suplee.Api.Controllers.Identidade.InputModels
{
    public class CadastrarEnderecoInputModel
    {
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
