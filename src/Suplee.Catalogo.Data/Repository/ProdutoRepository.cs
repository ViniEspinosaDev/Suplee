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

        public async Task<IEnumerable<Produto>> ObterProdutosPaginadoPorCategoria(
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

        public async Task<IEnumerable<Produto>> ObterProdutosPaginadoPorEfeito(
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
