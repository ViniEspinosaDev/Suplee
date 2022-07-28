using Microsoft.EntityFrameworkCore;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Catalogo.Domain.Models;
using Suplee.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _catalogoContext;

        public ProdutoRepository(CatalogoContext catalogoContext)
        {
            _catalogoContext = catalogoContext;
        }

        public IUnitOfWork UnitOfWork => _catalogoContext;

        public async Task<Categoria> ObterCategoria(Guid categoriaId)
        {
            return await _catalogoContext.Categorias.FindAsync(categoriaId);
        }

        public async Task<Efeito> ObterEfeito(Guid efeitoId)
        {
            return await _catalogoContext.Efeitos.FindAsync(efeitoId);
        }

        public async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            return await _catalogoContext.Categorias.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Efeito>> ObterEfeitos()
        {
            return await _catalogoContext.Efeitos.AsNoTracking().ToListAsync();
        }

        public async Task<Produto> ObterProduto(Guid produtoId)
        {
            return await _catalogoContext.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.InformacaoNutricional)
                    .ThenInclude(i => i.CompostosNutricionais)
                .Include(p => p.Imagens)
                .Include(p => p.Efeitos)
                    .ThenInclude(p => p.Efeito)
                .FirstOrDefaultAsync(p => p.Id == produtoId);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPaginado(int pagina = 0, int quantidade = 0)
        {
            var produtos = _catalogoContext.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Imagens)
                .Include(p => p.Efeitos)
                    .ThenInclude(e => e.Efeito)
                .AsNoTracking();

            if (pagina > 0)
                produtos = produtos.Skip((pagina - 1) * quantidade);

            if (quantidade > 0)
                produtos = produtos.Take(quantidade);

            return await produtos.ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPaginadoPorIdCategoria(
            Guid categoriaId,
            int pagina = 0,
            int quantidade = 0)
        {
            var produtos = _catalogoContext.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Imagens)
                .Include(p => p.Efeitos)
                    .ThenInclude(e => e.Efeito)
                .AsNoTracking()
                .Where(p => p.CategoriaId == categoriaId);

            if (pagina > 0)
                produtos = produtos.Skip((pagina - 1) * quantidade);

            if (quantidade > 0)
                produtos = produtos.Take(quantidade);

            return await produtos.ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPaginadoPorNomeCategoria(
            string nomeCategoria,
            int pagina = 0,
            int quantidade = 0)
        {
            var produtos = _catalogoContext.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Imagens)
                .Include(p => p.Efeitos)
                    .ThenInclude(e => e.Efeito)
                .AsNoTracking()
                .Where(p => p.Categoria.Nome == nomeCategoria);

            if (pagina > 0)
                produtos = produtos.Skip((pagina - 1) * quantidade);

            if (quantidade > 0)
                produtos = produtos.Take(quantidade);

            return await produtos.ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPaginadoPorIdEfeito(
            Guid efeitoId,
            int pagina = 0,
            int quantidade = 0)
        {
            var produtos = _catalogoContext.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Imagens)
                .Include(p => p.Efeitos)
                    .ThenInclude(e => e.Efeito)
                .AsNoTracking()
                .Where(p => p.Efeitos.Any(e => e.EfeitoId == efeitoId));

            if (pagina > 0)
                produtos = produtos.Skip((pagina - 1) * quantidade);

            if (quantidade > 0)
                produtos = produtos.Take(quantidade);

            return await produtos.ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPaginadoPorNomeEfeito(
            string nomeEfeito,
            int pagina = 0,
            int quantidade = 0)
        {
            var efeito = _catalogoContext.Efeitos.FirstOrDefault(e => e.Nome == nomeEfeito);

            if (efeito is null)
                return null;

            var produtos = _catalogoContext.Produtos
                  .Include(p => p.Categoria)
                  .Include(p => p.Imagens)
                  .Include(p => p.Efeitos)
                      .ThenInclude(e => e.Efeito)
                  .AsNoTracking()
                  .Where(p => p.Efeitos.Any(e => e.EfeitoId == efeito.Id));

            if (pagina > 0)
                produtos = produtos.Skip((pagina - 1) * quantidade);

            if (quantidade > 0)
                produtos = produtos.Take(quantidade);

            return await produtos.ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPaginadoPorNomeProduto(
            string nome,
            int pagina = 0,
            int quantidade = 0)
        {
            var produtos = _catalogoContext.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Imagens)
                .Include(p => p.Efeitos)
                    .ThenInclude(e => e.Efeito)
                .AsNoTracking()
                .Where(p => p.Nome.Contains(nome));

            if (pagina > 0)
                produtos = produtos.Skip((pagina - 1) * quantidade);

            if (quantidade > 0)
                produtos = produtos.Take(quantidade);

            return await produtos.ToListAsync();
        }

        public int QuantidadeTotalProdutos()
        {
            return _catalogoContext.Produtos.Count();
        }

        public int QuantidadeProdutosPeloNome(string nome)
        {
            return _catalogoContext.Produtos.Where(p => p.Nome.Contains(nome)).Count();
        }

        public int QuantidadeProdutosPeloNomeCategoria(string nomeCategoria)
        {
            return _catalogoContext.Produtos.Where(p => p.Categoria.Nome == nomeCategoria).Count();
        }

        public int QuantidadeProdutosPeloIdCategoria(Guid categoriaId)
        {
            return _catalogoContext.Produtos.Where(p => p.CategoriaId == categoriaId).Count();
        }

        public int QuantidadeProdutosPeloNomeEfeito(string nomeEfeito)
        {
            var efeito = _catalogoContext.Efeitos.FirstOrDefault(e => e.Nome == nomeEfeito);

            return _catalogoContext.Produtos.Where(p => p.Efeitos.Any(e => e.EfeitoId == efeito.Id)).Count();
        }

        public int QuantidadeProdutosPeloIdEfeito(Guid efeitoId)
        {
            return _catalogoContext.Produtos.Where(p => p.Efeitos.Any(e => e.EfeitoId == efeitoId)).Count();
        }

        public void Adicionar(Produto produto)
        {
            _catalogoContext.Produtos.Add(produto);
        }

        public void Adicionar(Categoria categoria)
        {
            _catalogoContext.Categorias.Add(categoria);
        }

        public void Adicionar(Efeito efeito)
        {
            _catalogoContext.Efeitos.Add(efeito);
        }

        public void Atualizar(Produto produto)
        {
            _catalogoContext.Produtos.Update(produto);
        }

        public void Atualizar(Categoria categoria)
        {
            _catalogoContext.Categorias.Update(categoria);
        }

        public void Atualizar(Efeito efeito)
        {
            _catalogoContext.Efeitos.Update(efeito);
        }

        public void Dispose()
        {
            _catalogoContext?.Dispose();
        }
    }
}
