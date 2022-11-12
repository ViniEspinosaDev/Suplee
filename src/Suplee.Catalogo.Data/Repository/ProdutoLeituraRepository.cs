using MongoDB.Bson;
using MongoDB.Driver;
using Suplee.Catalogo.Domain.DTO;
using Suplee.Catalogo.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Data.Repository
{
    public class ProdutoLeituraRepository : IProdutoLeituraRepository
    {
        private const string NomeBancoDeDados = "supleedatabase";
        private const string NomeColecao = "Produtos";

        private readonly IMongoCollection<ProdutoDTO> _produtos;
        private readonly FilterDefinitionBuilder<ProdutoDTO> _filtro = Builders<ProdutoDTO>.Filter;

        public ProdutoLeituraRepository(IMongoClient mongoClient)
        {
            var bancoDeDados = mongoClient.GetDatabase(NomeBancoDeDados);
            _produtos = bancoDeDados.GetCollection<ProdutoDTO>(NomeColecao);
        }

        public void AdicionarProduto(ProdutoDTO produto)
        {
            _produtos.InsertOne(produto);
        }

        public void AtualizarProduto(ProdutoDTO produto)
        {
            var filtro = _filtro.Eq(produtoExistente => produtoExistente.Id, produto.Id);

            _produtos.ReplaceOne(filtro, produto);
        }

        public void ExcluirProduto(Guid produtoId)
        {
            var filtro = _filtro.Eq(produtoExistente => produtoExistente.Id, produtoId);

            _produtos.DeleteOne(filtro);
        }

        public async Task<int> QuantidadeProdutosComEstoque()
        {
            var filtro = _filtro.Gt(produto => produto.QuantidadeDisponivel, 0);

            return (await _produtos.FindAsync(filtro)).ToList().Count();
        }

        public async Task<int> QuantidadeTotalProdutos()
        {
            return (await _produtos.FindAsync(new BsonDocument())).ToList().Count();
        }

        public async Task<ProdutoDTO> RecuperarProduto(Guid produtoId)
        {
            var filtro = _filtro.Eq(produto => produto.Id, produtoId);

            return await _produtos.Find(filtro).SingleOrDefaultAsync();
        }

        public async Task<List<ProdutoDTO>> RecuperarProdutos()
        {
            return (await _produtos.FindAsync(new BsonDocument())).ToList();
        }

        public async Task<List<ProdutoDTO>> RecuperarProdutosComEstoque()
        {
            var filtro = _filtro.Gt(produto => produto.QuantidadeDisponivel, 0);

            return (await _produtos.FindAsync(filtro)).ToList();
        }

        public async Task<List<ProdutoDTO>> RecuperarProdutosComEstoquePeloIdCategoria(Guid categoriaId)
        {
            var filtro = _filtro.Gt(produto => produto.QuantidadeDisponivel, 0);

            filtro &= Builders<ProdutoDTO>.Filter.Where(produto => produto.Categoria.Id == categoriaId);

            return (await _produtos.FindAsync(filtro)).ToList();
        }

        public async Task<List<ProdutoDTO>> RecuperarProdutosComEstoquePeloIdEfeito(Guid efeitoId)
        {
            var filtro = _filtro.Gt(produto => produto.QuantidadeDisponivel, 0);

            filtro &= Builders<ProdutoDTO>.Filter.Where(produto => produto.Efeitos.Any(efeito => efeito.Id == efeitoId));

            return (await _produtos.FindAsync(filtro)).ToList();
        }

        public async Task<List<ProdutoDTO>> RecuperarProdutosComEstoquePeloNome(string nome)
        {
            var filtro = _filtro.Gt(produto => produto.QuantidadeDisponivel, 0);

            filtro &= Builders<ProdutoDTO>.Filter.Where(produto => produto.Nome.ToLower().Contains(nome.ToLower()));

            return (await _produtos.FindAsync(filtro)).ToList();
        }

        public async Task<List<ProdutoDTO>> RecuperarProdutosComEstoquePeloNomeCategoria(string nomeCategoria)
        {
            var filtro = _filtro.Gt(produto => produto.QuantidadeDisponivel, 0);

            filtro &= Builders<ProdutoDTO>.Filter.Where(produto => produto.Categoria.Nome.ToLower().Contains(nomeCategoria.ToLower()));

            return (await _produtos.FindAsync(filtro)).ToList();
        }

        public async Task<List<ProdutoDTO>> RecuperarProdutosComEstoquePeloNomeEfeito(string nomeEfeito)
        {
            var filtro = _filtro.Gt(produto => produto.QuantidadeDisponivel, 0);

            filtro &= Builders<ProdutoDTO>.Filter.Where(produto => produto.Efeitos.Any(efeito => efeito.Nome.ToLower().Contains(nomeEfeito.ToLower())));

            return (await _produtos.FindAsync(filtro)).ToList();
        }

        public async Task<List<ProdutoDTO>> RecuperarProdutosComEstoquePeloNomeProduto(string nomeProduto)
        {
            var filtro = _filtro.Gt(produto => produto.QuantidadeDisponivel, 0);

            filtro &= Builders<ProdutoDTO>.Filter.Where(produto => produto.Nome.ToLower().Contains(nomeProduto.ToLower()));

            return (await _produtos.FindAsync(filtro)).ToList();
        }
    }
}
