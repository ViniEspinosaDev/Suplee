using Suplee.Catalogo.Data.Repository;
using Suplee.Catalogo.Domain.Enums;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Catalogo.Domain.Models;
using Suplee.Test.Builder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Suplee.Test.Repositories.Catalogo
{
    public class ProdutoRepositoryTest : CatalogoRepositoryBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoRepositoryTest() : base()
        {
            _produtoRepository = new ProdutoRepository(DbContext);
        }

        [Fact]
        public void Deve_Testar_Instancia_Repository()
        {
            Assert.True(_produtoRepository != null);
        }

        [Fact]
        public async void Deve_Criar_Uma_Categoria()
        {
            var categoria = new CategoriaBuilder()
                .PadraoValido()
                .Build();

            _produtoRepository.Adicionar(categoria);

            DbContext.SaveChanges();

            var categoriaAdicionada = await _produtoRepository.ObterCategoria(categoria.Id);

            Assert.NotNull(categoriaAdicionada);
            Assert.Equal(categoria.Nome, categoriaAdicionada.Nome);
            Assert.Equal(categoria.Descricao, categoriaAdicionada.Descricao);
            Assert.Equal(categoria.Icone, categoriaAdicionada.Icone);
            Assert.Equal(categoria.Cor, categoriaAdicionada.Cor);
        }

        [Fact]
        public async void Deve_Atualizar_Uma_Categoria()
        {
            var categoria = new CategoriaBuilder()
                .PadraoValido()
                .Build();

            _produtoRepository.Adicionar(categoria);

            DbContext.SaveChanges();

            var categoriaAdicionada = await _produtoRepository.ObterCategoria(categoria.Id);

            categoriaAdicionada.Atualizar(nome: "Novo Nome", descricao: "Nova Descrição", icone: "Novo Icone", cor: ECor.GreenLight);

            _produtoRepository.Atualizar(categoriaAdicionada);

            DbContext.SaveChanges();

            var categoriaAtualizada = await _produtoRepository.ObterCategoria(categoria.Id);

            Assert.Equal("Novo Nome", categoriaAtualizada.Nome);
            Assert.Equal("Nova Descrição", categoriaAtualizada.Descricao);
            Assert.Equal("Novo Icone", categoriaAtualizada.Icone);
            Assert.Equal(ECor.GreenLight, categoriaAtualizada.Cor);
        }

        [Fact]
        public async void Deve_Criar_Um_Efeito()
        {
            var efeito = new EfeitoBuilder()
                .PadraoValido()
                .Build();

            _produtoRepository.Adicionar(efeito);

            DbContext.SaveChanges();

            var efeitoAdicionado = await _produtoRepository.ObterEfeito(efeito.Id);

            Assert.NotNull(efeitoAdicionado);
            Assert.Equal(efeito.Nome, efeitoAdicionado.Nome);
            Assert.Equal(efeito.Descricao, efeitoAdicionado.Descricao);
            Assert.Equal(efeito.Icone, efeitoAdicionado.Icone);
        }

        [Fact]
        public async void Deve_Atualizar_Um_Efeito()
        {
            var efeito = new EfeitoBuilder()
                .PadraoValido()
                .Build();

            _produtoRepository.Adicionar(efeito);

            DbContext.SaveChanges();

            var efeitoAdicionado = await _produtoRepository.ObterEfeito(efeito.Id);

            efeitoAdicionado.Atualizar(nome: "Novo Nome", descricao: "Nova Descrição", icone: "Novo Icone");

            _produtoRepository.Atualizar(efeitoAdicionado);

            DbContext.SaveChanges();

            var efeitoAtualizado = await _produtoRepository.ObterEfeito(efeito.Id);

            Assert.Equal("Novo Nome", efeitoAtualizado.Nome);
            Assert.Equal("Nova Descrição", efeitoAtualizado.Descricao);
            Assert.Equal("Novo Icone", efeitoAtualizado.Icone);
        }

        [Fact]
        public async void Deve_Criar_Um_Produto()
        {
            var produtoId = Guid.NewGuid();

            var categoria = new CategoriaBuilder().PadraoValido().Build();

            var efeito = new EfeitoBuilder().PadraoValido().Build();

            var efeitos = new List<ProdutoEfeito>() { new ProdutoEfeitoBuilder().PadraoValido(produtoId, categoria.Id).Build() };
            var imagens = new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().ComProdutoId(produtoId).Build() };

            var produto = new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(categoria)
                .ComEfeitos(efeitos)
                .ComImagens(imagens)
                .Build();

            _produtoRepository.Adicionar(efeito);
            _produtoRepository.Adicionar(produto);

            DbContext.SaveChanges();

            var produtoAdicionado = await _produtoRepository.ObterProduto(produto.Id);

            Assert.Equal(produto.Nome, produtoAdicionado.Nome);
            Assert.Equal(produto.QuantidadeDisponivel, produtoAdicionado.QuantidadeDisponivel);
            Assert.Equal(produto.Categoria.Id, produtoAdicionado.Categoria.Id);
            Assert.Equal(produto.InformacaoNutricional.Id, produtoAdicionado.InformacaoNutricional.Id);
            Assert.Equal(1, produtoAdicionado.InformacaoNutricional.CompostosNutricionais.Count);
            Assert.Equal(efeitos.FirstOrDefault().Id, produtoAdicionado.Efeitos.FirstOrDefault().Id);
            Assert.Equal(imagens.FirstOrDefault().Id, produtoAdicionado.Imagens.FirstOrDefault().Id);
        }

        [Fact]
        public async void Deve_Atualizar_Um_Produto()
        {
            var produtoId = Guid.NewGuid();

            var categoria = new CategoriaBuilder().PadraoValido().Build();

            var efeito = new EfeitoBuilder().PadraoValido().Build();

            var efeitos = new List<ProdutoEfeito>() { new ProdutoEfeitoBuilder().PadraoValido(produtoId, categoria.Id).Build() };
            var imagens = new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().ComProdutoId(produtoId).Build() };

            var produto = new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(categoria)
                .ComEfeitos(efeitos)
                .ComImagens(imagens)
                .Build();

            _produtoRepository.Adicionar(efeito);
            _produtoRepository.Adicionar(produto);

            DbContext.SaveChanges();

            produto.Atualizar("Nome atualizado", "Descrição atualizada", "Composição atualizada", 10, 10.99m);
            
            _produtoRepository.Atualizar(produto);

            DbContext.SaveChanges();

            var produtoAtualizado = await _produtoRepository.ObterProduto(produto.Id);

            Assert.Equal("Nome atualizado", produtoAtualizado.Nome);
            Assert.Equal("Descrição atualizada", produtoAtualizado.Descricao);
            Assert.Equal("Composição atualizada", produtoAtualizado.Composicao);
            Assert.Equal(10, produtoAtualizado.QuantidadeDisponivel);
            Assert.Equal(10.99m, produtoAtualizado.Preco);
        }

        // Deve_Obter_Produto_Pelo_Id (Guid produtoId);
        // Deve_Obter_Categoria_Pelo_Id (Guid categoriaId);
        // Deve_Obter_Efeito_Pelo_Id (Guid efeitoId);
        // Deve_Obter_Produtos_Paginado (int pagina, int quantidade);
        // Deve_Obter_Produtos_Paginado_Pelo_Id_Categoria (Guid categoriaId, int pagina, int quantidade);
        // Deve_Obter_Produtos_Paginado_Pelo_Nome_Categoria (string nomeCategoria, int pagina, int quantidade);
        // Deve_Obter_Produtos_Paginado_Pelo_Id_Efeito (Guid efeitoId, int pagina, int quantidade);
        // Deve_Obter_Produtos_Paginado_Pelo_Nome_Efeito (string nomeEfeito, int pagina, int quantidade);
        // Deve_Obter_Produtos_Paginado_Pelo_Nome_Produto (string nome, int pagina, int quantidade);

        // Deve_Obter_Categorias();
        // Deve_Obter_Efeitos();

    }
}
