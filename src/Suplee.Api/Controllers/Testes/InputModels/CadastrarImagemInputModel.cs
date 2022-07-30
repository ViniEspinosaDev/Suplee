using Microsoft.AspNetCore.Http;

namespace Suplee.Api.Controllers.Testes.InputModels
{
    /// <summary>
    /// Cadastrar imagem
    /// </summary>
    public class CadastrarImagemInputModel
    {
        /// <summary>
        /// Imagem
        /// </summary>
        public IFormFile Imagem { get; set; }
    }
}
