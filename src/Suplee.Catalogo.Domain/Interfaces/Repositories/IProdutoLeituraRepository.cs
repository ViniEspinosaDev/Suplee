using Suplee.Catalogo.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Domain.Interfaces.Repositories
{
    public interface IProdutoLeituraRepository
    {
        void AdicionarProduto(ProdutoDTO produto);
        void AtualizarProduto(ProdutoDTO produto);

        Task<int> QuantidadeTotalProdutos();
        Task<int> QuantidadeProdutosComEstoque();

        Task<ProdutoDTO> RecuperarProduto(Guid produtoId);
        Task<List<ProdutoDTO>> RecuperarProdutos();
        Task<List<ProdutoDTO>> RecuperarProdutosComEstoque();
        Task<List<ProdutoDTO>> RecuperarProdutosComEstoquePeloNome(string nome);
        Task<List<ProdutoDTO>> RecuperarProdutosComEstoquePeloNomeCategoria(string nomeCategoria);
        Task<List<ProdutoDTO>> RecuperarProdutosComEstoquePeloIdCategoria(Guid categoriaId);
        Task<List<ProdutoDTO>> RecuperarProdutosComEstoquePeloNomeEfeito(string nomeEfeito);
        Task<List<ProdutoDTO>> RecuperarProdutosComEstoquePeloIdEfeito(Guid efeitoId);
        Task<List<ProdutoDTO>> RecuperarProdutosComEstoquePeloNomeProduto(string nomeProduto);
    }
}
