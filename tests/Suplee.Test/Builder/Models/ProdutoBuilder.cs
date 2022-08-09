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
            var categoria = new CategoriaBuilder().PadraoValido().Build();
            var random = new Random();
            var efeito = new EfeitoBuilder().PadraoValido().ComNome($"{random.Next()}_{random.Next()}").Build();
            var produtoEfeito = new ProdutoEfeitoBuilder().PadraoValido(Id, efeito.Id).ComEfeito(efeito).Build();

            InformacaoNutricionalId = informacaoNutricional.Id;
            InformacaoNutricional = informacaoNutricional;
            CategoriaId = categoria.Id;
            Categoria = categoria;
            Nome = "Nome";
            Descricao = "Descrição";
            Composicao = "Composição";
            QuantidadeDisponivel = 1;
            Preco = 12.40m;
            Dimensoes = new Dimensoes(10m, 10m, 10m);
            Efeitos.Add(produtoEfeito);

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
            efeitos.ForEach(x => Efeitos.Add(x));

            return this;
        }

        public ProdutoBuilder ComImagens(List<ProdutoImagem> imagens)
        {
            Imagens = imagens;

            return this;
        }

        public ProdutoBuilder ComNome(string nome)
        {
            Nome = nome;

            return this;
        }

        public Produto Build() => this;
    }
}
