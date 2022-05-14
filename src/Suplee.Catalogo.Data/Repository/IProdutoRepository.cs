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

        public async Task<IEnumerable<Produto>> ObterProdutos()
        {
            return await _catalogoContext.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.InformacaoNutricional)
                    .ThenInclude(i => i.CompostosNutricionais)
                .Include(p => p.Imagens)
                .Include(p => p.Efeitos)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorCategoria(Guid categoriaId)
        {
            return await _catalogoContext.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.InformacaoNutricional)
                    .ThenInclude(i => i.CompostosNutricionais)
                .Include(p => p.Imagens)
                .Include(p => p.Efeitos)
                .AsNoTracking()
                .Where(p => p.CategoriaId == categoriaId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorEfeito(Guid efeitoId)
        {
            return await _catalogoContext.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.InformacaoNutricional)
                    .ThenInclude(i => i.CompostosNutricionais)
                .Include(p => p.Imagens)
                .Include(p => p.Efeitos)
                .AsNoTracking()
                .Where(p => p.Efeitos.Any(e => e.EfeitoId == efeitoId))
                .ToListAsync();
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
