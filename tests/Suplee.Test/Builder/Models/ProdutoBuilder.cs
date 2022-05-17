using Suplee.Catalogo.Domain.Models;
using Suplee.Catalogo.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Suplee.Test.Builder.Models
{
    public class ProdutoBuilder : Produto
    {
        public ProdutoBuilder(
            Guid informacaoNutricionalId = default,
            Guid categoriaId = default,
            string nome = default,
            string descricao = default,
            string composicao = default,
            int quantidadeDisponivel = default,
            decimal preco = default,
            Dimensoes dimensoes = default) : base(informacaoNutricionalId, categoriaId, nome, descricao, composicao, quantidadeDisponivel, preco, dimensoes)
        {
        }

        public ProdutoBuilder PadraoValido()
        {
            var informacaoNutricional = new InformacaoNutricionalBuilder()
                .PadraoValido()
                .ComCompostosNutricionais()
                .Build();

            InformacaoNutricionalId = informacaoNutricional.Id;
            CategoriaId = Guid.Empty;
            Nome = "Nome";
            Descricao = "Descrição";
            Composicao = "Composição";
            QuantidadeDisponivel = 1;
            Preco = 12.40m;
            Dimensoes = new Dimensoes(10m, 10m, 10m);

            return this;
        }

        public ProdutoBuilder ComCategoria(Categoria categoria)
        {
            CategoriaId = categoria.Id;
            Categoria = categoria;

            return this;
        }

        public ProdutoBuilder ComEfeitos(List<ProdutoEfeito> efeitos)
        {
            Efeitos = efeitos;

            return this;
        }

        public ProdutoBuilder ComImagens(List<ProdutoImagem> imagens)
        {
            Imagens = imagens;

            return this;
        }

        public Produto Build() => this;
    }
}
