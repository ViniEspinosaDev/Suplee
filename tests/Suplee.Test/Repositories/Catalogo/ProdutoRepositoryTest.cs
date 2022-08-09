using Suplee.Catalogo.Data;
using Suplee.Catalogo.Data.Repository;
using Suplee.Catalogo.Domain.Enums;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Catalogo.Domain.Models;
using Suplee.Test.Builder.Models;
using Suplee.Test.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Suplee.Test.Repositories.Catalogo
{
    public class ProdutoRepositoryTest
    {
        private readonly CatalogoContext _context;
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoRepositoryTest()
        {
            _context = StubContextDomain.GetDatabaseContextCatalogo();
            _produtoRepository = new ProdutoRepository(_context);
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

            AdicionarCategoria(categoria);

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

            AdicionarCategoria(categoria);

            var categoriaAdicionada = await _produtoRepository.ObterCategoria(categoria.Id);

            categoriaAdicionada.Atualizar(nome: "Novo Nome", descricao: "Nova Descrição", icone: "Novo Icone", cor: ECor.GreenLight);

            await _produtoRepository.UnitOfWork.Commit();

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

            AdicionarEfeito(efeito);

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

            AdicionarEfeito(efeito);

            var efeitoAdicionado = await _produtoRepository.ObterEfeito(efeito.Id);

            efeitoAdicionado.Atualizar(nome: "Novo Nome", descricao: "Nova Descrição", icone: "Novo Icone");

            await _produtoRepository.UnitOfWork.Commit();

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

            var efeitos = new List<ProdutoEfeito>() { new ProdutoEfeitoBuilder().PadraoValido(produtoId, efeito.Id).ComEfeito(efeito).Build() };
            var imagens = new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().ComProdutoId(produtoId).Build() };

            var produto = new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(categoria)
                .ComEfeitos(efeitos)
                .ComImagens(imagens)
                .Build();

            AdicionarProduto(produto);

            var produtoAdicionado = await _produtoRepository.ObterProduto(produto.Id);

            Assert.Equal(produto.Nome, produtoAdicionado.Nome);
            Assert.Equal(produto.QuantidadeDisponivel, produtoAdicionado.QuantidadeDisponivel);
            Assert.Equal(produto.Categoria.Id, produtoAdicionado.Categoria.Id);
            Assert.Equal(produto.InformacaoNutricional.Id, produtoAdicionado.InformacaoNutricional.Id);
            Assert.Equal(1, produtoAdicionado.InformacaoNutricional.CompostosNutricionais.Count);
            Assert.Contains(produtoAdicionado.Efeitos, x => x.Id == efeitos.FirstOrDefault().Id);
            Assert.Equal(imagens.FirstOrDefault().Id, produtoAdicionado.Imagens.FirstOrDefault().Id);
        }

        [Fact]
        public async void Deve_Atualizar_Um_Produto()
        {
            var produtoId = Guid.NewGuid();

            var categoria = new CategoriaBuilder().PadraoValido().Build();

            var efeitos = new List<ProdutoEfeito>() { new ProdutoEfeitoBuilder().PadraoValido(produtoId, categoria.Id).Build() };
            var imagens = new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().ComProdutoId(produtoId).Build() };

            var produto = new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(categoria)
                .ComEfeitos(efeitos)
                .ComImagens(imagens)
                .Build();

            AdicionarProduto(produto);

            produto.Atualizar("Nome atualizado", "Descrição atualizada", "Composição atualizada", 10, 10.99m);

            await _produtoRepository.UnitOfWork.Commit();

            var produtoAtualizado = await _produtoRepository.ObterProduto(produto.Id);

            Assert.Equal("Nome atualizado", produtoAtualizado.Nome);
            Assert.Equal("Descrição atualizada", produtoAtualizado.Descricao);
            Assert.Equal("Composição atualizada", produtoAtualizado.Composicao);
            Assert.Equal(10, produtoAtualizado.QuantidadeDisponivel);
            Assert.Equal(10.99m, produtoAtualizado.Preco);
        }

        [Fact]
        public async void Deve_Obter_Produto_Pelo_Id()
        {
            var categoria = new CategoriaBuilder().PadraoValido().Build();
            var imagens = new ProdutoImagemBuilder().PadraoValido().Build();

            var produto = new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(categoria)
                .ComImagens(new List<ProdutoImagem>() { imagens })
                .Build();

            AdicionarProduto(produto);

            var produtoAdicionado = await _produtoRepository.ObterProduto(produto.Id);

            Assert.NotNull(produtoAdicionado);
            Assert.NotNull(produtoAdicionado.Categoria);
            Assert.NotNull(produtoAdicionado.Efeitos);
            Assert.NotNull(produtoAdicionado.Imagens);
        }

        [Fact]
        public async void Deve_Obter_Categoria_Pelo_Id()
        {
            var categoria = new CategoriaBuilder().PadraoValido().Build();

            AdicionarCategoria(categoria);

            var categoriaAdicionada = await _produtoRepository.ObterCategoria(categoria.Id);

            Assert.NotNull(categoriaAdicionada);
        }

        [Fact]
        public async void Deve_Obter_Efeito_Pelo_Id()
        {
            var efeito = new EfeitoBuilder().PadraoValido().Build();

            AdicionarEfeito(efeito);

            var efeitoAdicionada = await _produtoRepository.ObterEfeito(efeito.Id);

            Assert.NotNull(efeitoAdicionada);
        }

        [Fact]
        public async void Deve_Obter_Produto_Paginado()
        {
            var produtos = CriarProdutos();

            produtos.ForEach(x => AdicionarProduto(x));

            var produtosPaginado = await _produtoRepository.ObterProdutosPaginado(2, 3);

            Assert.NotNull(produtosPaginado);
            Assert.Equal(3, produtosPaginado.Count());
        }

        [Fact]
        public async void Deve_Obter_Produtos_Paginado_Pelo_Id_Categoria()
        {
            var categoria = new CategoriaBuilder().PadraoValido().Build();

            AdicionarCategoria(categoria);

            var produtos = CriarProdutosComCategoria(categoria);

            produtos.ForEach(x => AdicionarProduto(x));

            var produtosPaginados = await _produtoRepository.ObterProdutosPaginadoPorIdCategoria(categoria.Id, 1, 5);

            Assert.NotNull(produtosPaginados);
            Assert.Equal(4, produtosPaginados.Count());
        }

        [Fact]
        public async void Deve_Obter_Produtos_Paginado_Pelo_Nome_Categoria()
        {
            var categoria = new CategoriaBuilder().PadraoValido().ComNome("Categoria 1").Build();

            AdicionarCategoria(categoria);

            var produtos = CriarProdutosComCategoria(categoria);

            produtos.ForEach(x => AdicionarProduto(x));

            var produtosPaginados = await _produtoRepository.ObterProdutosPaginadoPorNomeCategoria(categoria.Nome, 1, 5);

            Assert.NotNull(produtosPaginados);
            Assert.Equal(4, produtosPaginados.Count());
        }

        [Fact]
        public async void Deve_Obter_Produtos_Paginado_Pelo_Id_Efeito()
        {
            var efeito = new EfeitoBuilder().PadraoValido().ComNome("Efeito Borboleta").Build();
            var produtoEfeito = new ProdutoEfeitoBuilder().PadraoValido(Guid.Empty, efeito.Id).ComEfeito(efeito).Build();

            var produtos = CriarProdutosComEfeito(produtoEfeito);

            produtos.ForEach(x => AdicionarProduto(x));

            var produtosPaginados = await _produtoRepository.ObterProdutosPaginadoPorIdEfeito(efeito.Id, 0, 0);

            Assert.NotNull(produtosPaginados);
            Assert.Single(produtosPaginados);
        }

        [Fact]
        public async void Deve_Obter_Produtos_Paginado_Pelo_Nome_Efeito()
        {
            var efeito = new EfeitoBuilder().PadraoValido().ComNome("Efeito Borboleta").Build();
            var produtoEfeito = new ProdutoEfeitoBuilder().PadraoValido(Guid.Empty, efeito.Id).ComEfeito(efeito).Build();

            var produtos = CriarProdutosComEfeito(produtoEfeito);

            produtos.ForEach(x => AdicionarProduto(x));

            var produtosPaginados = await _produtoRepository.ObterProdutosPaginadoPorNomeEfeito(efeito.Nome, 1, 6);

            Assert.NotNull(produtosPaginados);
            Assert.Single(produtosPaginados);
        }

        [Fact]
        public async void Deve_Obter_Produtos_Paginados_Pelo_Nome_Produto()
        {
            var produto = new ProdutoBuilder().PadraoValido().ComNome("Produtão").Build();
            var produto2 = new ProdutoBuilder().PadraoValido().ComNome("Produton").Build();

            AdicionarProduto(produto);
            AdicionarProduto(produto2);

            var produtoAdicionado = await _produtoRepository.ObterProdutosPaginadoPorNomeProduto(produto.Nome, 1, 3);

            Assert.NotNull(produtoAdicionado);
            Assert.Single(produtoAdicionado);
        }

        [Fact]
        public async void Deve_Obter_Categorias()
        {
            var categorias = new List<Categoria>()
            {
                new CategoriaBuilder().PadraoValido().ComNome("Categoria 1").Build(),
                new CategoriaBuilder().PadraoValido().ComNome("Categoria 2").Build(),
                new CategoriaBuilder().PadraoValido().ComNome("Categoria 3").Build(),
                new CategoriaBuilder().PadraoValido().ComNome("Categoria 4").Build(),
            };

            categorias.ForEach(x => AdicionarCategoria(x));

            var categoriasAdicionadas = await _produtoRepository.ObterCategorias();

            Assert.NotNull(categoriasAdicionadas);
            Assert.Equal(categorias.Count(), categoriasAdicionadas.Count());
        }

        [Fact]
        public async void Deve_Obter_Efeitos()
        {
            var efeitos = new List<Efeito>()
            {
                new EfeitoBuilder().PadraoValido().ComNome("Efeito 1").Build(),
                new EfeitoBuilder().PadraoValido().ComNome("Efeito 2").Build(),
                new EfeitoBuilder().PadraoValido().ComNome("Efeito 3").Build(),
                new EfeitoBuilder().PadraoValido().ComNome("Efeito 4").Build(),
            };

            efeitos.ForEach(x => AdicionarEfeito(x));

            var efeitosAdicionados = await _produtoRepository.ObterEfeitos();

            Assert.NotNull(efeitosAdicionados);
            Assert.Equal(efeitos.Count(), efeitosAdicionados.Count());
        }

        #region Métodos auxiliares
        private List<Produto> CriarProdutos()
        {
            return new List<Produto>()
            {
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(new CategoriaBuilder().PadraoValido().Build())
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .Build(),
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(new CategoriaBuilder().PadraoValido().Build())
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .Build(),
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(new CategoriaBuilder().PadraoValido().Build())
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .Build(),
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(new CategoriaBuilder().PadraoValido().Build())
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .Build(),
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(new CategoriaBuilder().PadraoValido().Build())
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .Build(),
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(new CategoriaBuilder().PadraoValido().Build())
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .Build()
            };
        }

        public List<Produto> CriarProdutosComCategoria(Categoria categoria)
        {
            return new List<Produto>()
            {
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(categoria)
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .Build(),
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(categoria)
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .Build(),
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(categoria)
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .Build(),
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(categoria)
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .Build(),
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(new CategoriaBuilder().PadraoValido().Build())
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .Build(),
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(new CategoriaBuilder().PadraoValido().Build())
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .Build()
            };
        }

        public List<Produto> CriarProdutosComEfeito(ProdutoEfeito efeito)
        {
            return new List<Produto>()
            {
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(new CategoriaBuilder().PadraoValido().Build())
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .ComEfeitos(new List<ProdutoEfeito>() { efeito })
                .Build(),
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(new CategoriaBuilder().PadraoValido().Build())
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .ComEfeitos(new List<ProdutoEfeito>() { efeito })
                .Build(),
                new ProdutoBuilder()
                .PadraoValido()
                .ComCategoria(new CategoriaBuilder().PadraoValido().Build())
                .ComImagens(new List<ProdutoImagem>() { new ProdutoImagemBuilder().PadraoValido().Build() })
                .Build(),
            };
        }

        private void AdicionarProduto(Produto produto)
        {
            _produtoRepository.Adicionar(produto);

            _produtoRepository.UnitOfWork.Commit();
        }

        private void AdicionarEfeito(Efeito efeito)
        {
            _produtoRepository.Adicionar(efeito);

            _produtoRepository.UnitOfWork.Commit();
        }

        private void AdicionarCategoria(Categoria categoria)
        {
            _produtoRepository.Adicionar(categoria);

            _produtoRepository.UnitOfWork.Commit();
        }
        #endregion
    }
}