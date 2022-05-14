using Microsoft.AspNetCore.Mvc;
using Suplee.Catalogo.Domain.Interfaces;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public CatalogoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<ActionResult> ObterProdutos()
        {
            var produtos = await _produtoRepository.ObterProdutos();

            if (produtos is null)
                return BadRequest();

            return Ok(produtos);
        }

        [HttpGet("efeitos")]
        public async Task<ActionResult> ObterEfeitos()
        {
            var efeitos = await _produtoRepository.ObterEfeitos();

            if (efeitos is null)
                return BadRequest();

            return Ok(efeitos);
        }

        [HttpGet("categorias")]
        public async Task<ActionResult> ObterCategorias()
        {
            var efeitos = await _produtoRepository.ObterCategorias();

            if (efeitos is null)
                return BadRequest();

            return Ok(efeitos);
        }
    }
}
