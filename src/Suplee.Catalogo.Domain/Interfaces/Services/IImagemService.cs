using Microsoft.AspNetCore.Http;
using Suplee.Catalogo.Domain.Models;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Interfaces.Services
{
    public interface IImagemService
    {
        Task<ProdutoImagem> UploadImagem(IFormFile imagem);
    }
}
