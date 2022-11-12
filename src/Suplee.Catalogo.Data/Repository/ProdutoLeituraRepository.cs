using MongoDB.Bson;
using MongoDB.Driver;
using Suplee.Catalogo.Domain.DTO;
using Suplee.Catalogo.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

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

        public ProdutoDTO RecuperarProduto(Guid produtoId)
        {
            var filtro = _filtro.Eq(produto => produto.Id, produtoId);

            return _produtos.Find(filtro).SingleOrDefault();
        }

        public List<ProdutoDTO> RecuperarProdutosComEstoque()
        {
            return _produtos.Find(new BsonDocument()).ToList();
        }
    }
}
