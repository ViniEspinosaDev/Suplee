using Suplee.Catalogo.Domain.ValueObjects;
using Suplee.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace Suplee.Catalogo.Domain.Models
{
    public class Produto : Entity, IAggregateRoot
    {
        protected Produto() 
        {
            Efeitos = new List<ProdutoEfeito>();
            Imagens = new List<ProdutoImagem>();
        }

        public Produto(
            Guid informacaoNutricionalId,
            Guid categoriaId,
            string nome,
            string descricao,
            string composicao,
            int quantidadeDisponivel,
            decimal preco,
            Dimensoes dimensoes) : this()
        {
            InformacaoNutricionalId = informacaoNutricionalId;
            CategoriaId = categoriaId;
            Nome = nome;
            Descricao = descricao;
            Composicao = composicao;
            QuantidadeDisponivel = quantidadeDisponivel;
            Preco = preco;
            Dimensoes = dimensoes;
        }

        public Guid InformacaoNutricionalId { get; protected set; }
        public Guid CategoriaId { get; protected set; }
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        public string Composicao { get; protected set; }
        public int QuantidadeDisponivel { get; protected set; }
        public decimal Preco { get; protected set; }
        public Dimensoes Dimensoes { get; protected set; }

        public Categoria Categoria { get; protected set; }
        public InformacaoNutricional InformacaoNutricional { get; protected set; }
        public ICollection<ProdutoEfeito> Efeitos { get; protected set; }
        public ICollection<ProdutoImagem> Imagens { get; protected set; }

        public void AdicionarProdutoEfeito(ProdutoEfeito produtoEfeito) => Efeitos.Add(produtoEfeito);
        public void AdicionarProdutoImagem(ProdutoImagem produtoImagem) => Imagens.Add(produtoImagem);
        public void AdicionarInformacaoNutricional(InformacaoNutricional informacaoNutricional) => InformacaoNutricional = informacaoNutricional;
    }
}
