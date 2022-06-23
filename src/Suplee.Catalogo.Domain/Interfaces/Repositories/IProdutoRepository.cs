using Suplee.Catalogo.Domain.Models;
using Suplee.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> ObterProduto(Guid produtoId);
        Task<Categoria> ObterCategoria(Guid categoriaId);
        Task<Efeito> ObterEfeito(Guid efeitoId);
        Task<IEnumerable<Produto>> ObterProdutosPaginado(int pagina, int quantidade);
        Task<IEnumerable<Produto>> ObterProdutosPaginadoPorIdCategoria(Guid categoriaId, int pagina, int quantidade);
        Task<IEnumerable<Produto>> ObterProdutosPaginadoPorNomeCategoria(string nomeCategoria, int pagina, int quantidade);
        Task<IEnumerable<Produto>> ObterProdutosPaginadoPorIdEfeito(Guid efeitoId, int pagina, int quantidade);
        Task<IEnumerable<Produto>> ObterProdutosPaginadoPorNomeEfeito(string nomeEfeito, int pagina, int quantidade);
        Task<IEnumerable<Produto>> ObterProdutosPaginadoPorNomeProduto(string nome, int pagina, int quantidade);

        Task<IEnumerable<Categoria>> ObterCategorias();
        Task<IEnumerable<Efeito>> ObterEfeitos();

        int QuantidadeTotalProdutos();
        int QuantidadeProdutosPeloNome(string nome);
        int QuantidadeProdutosPeloNomeCategoria(string nomeCategoria);
        int QuantidadeProdutosPeloIdCategoria(Guid categoriaId);
        int QuantidadeProdutosPeloNomeEfeito(string nomeEfeito);
        int QuantidadeProdutosPeloIdEfeito(Guid efeitoId);

        void Adicionar(Produto produto);
        void Atualizar(Produto produto);
        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Adicionar(Efeito efeito);
        void Atualizar(Efeito efeito);
    }
}
