﻿using Suplee.Catalogo.Domain.ValueObjects;
using Suplee.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace Suplee.Catalogo.Domain.Models
{
    public class Produto : Entity, IAggregateRoot
    {
        public Produto(
            Guid informacaoNutricionalId,
            Guid categoriaId,
            string nome,
            string descricao,
            string composicao,
            int quantidadeDisponivel,
            decimal preco,
            Dimensoes dimensoes)
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
        public ICollection<Efeito> Efeitos { get; protected set; }

        public ICollection<ProdutoImagem> Imagens { get; protected set; }
        public Categoria Categoria { get; protected set; }
    }
}
