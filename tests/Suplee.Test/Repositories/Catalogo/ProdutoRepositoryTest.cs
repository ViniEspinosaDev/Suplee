using Suplee.Catalogo.Data.Repository;
using Suplee.Catalogo.Domain.Enums;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Test.Builder.Models;
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

            var categorias = await _produtoRepository.ObterCategorias();

            Assert.NotNull(categorias);
            Assert.Equal(categoria.Nome, categorias.FirstOrDefault().Nome);
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
    }
}
