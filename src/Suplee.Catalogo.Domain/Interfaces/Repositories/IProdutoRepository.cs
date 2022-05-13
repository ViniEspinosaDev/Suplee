﻿using Suplee.Catalogo.Domain.Models;
using Suplee.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> ObterProduto(Guid produtoId);
        Task<IEnumerable<Produto>> ObterProdutos();
        Task<IEnumerable<Produto>> ObterProdutosPorCategoria(Guid categoriaId);
        Task<IEnumerable<Produto>> ObterProdutosPorEfeito(Guid efeitoId);
        Task<IEnumerable<Produto>> ObterProdutosPorCategoriaEfeito(Guid categoriaId, Guid efeitoId);
        Task<IEnumerable<Categoria>> ObterCategorias();
        Task<IEnumerable<Efeito>> ObterEfeitos();

        void Adicionar(Produto produto);
        void Atualizar(Produto produto);
        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categeoria);
        void Adicionar(Efeito efeito);
        void Atualizar(Efeito efeito);
    }
}
