using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Suplee.Api.Controllers.Testes.InputModels
{
    /// <summary>
    /// Imagens
    /// </summary>
    public class CadastrarImagensInputModel
    {
        /// <summary>
        /// Imagens
        /// </summary>
        public List<ImagemInputModel> Imagens { get; set; }
    }
    /// <summary>
    /// Imagem
    /// </summary>
    public class ImagemInputModel
    {
        /// <summary>
        /// Imagem
        /// </summary>
        public IFormFile Imagem { get; set; }
    }
}
