using FluentValidation;
using Suplee.Catalogo.Domain.Models;
using Suplee.Catalogo.Domain.ValueObjects;
using Suplee.Core.Messages;
using System;
using System.Collections.Generic;

namespace Suplee.Catalogo.Domain.Commands
{
    public class AdicionarProdutoCommand : Command
    {
        public AdicionarProdutoCommand(
            Guid categoriaId,
            string nome,
            string descricao,
            string composicao,
            int quantidadeDisponivel,
            decimal preco,
            Dimensoes dimensoes,
            List<string> imagens,
            List<Guid> efeitos,
            InformacaoNutricional informacaoNutricional)
        {
            CategoriaId = categoriaId;
            Nome = nome;
            Descricao = descricao;
            Composicao = composicao;
            QuantidadeDisponivel = quantidadeDisponivel;
            Preco = preco;
            Dimensoes = dimensoes;
            Imagens = imagens;
            Efeitos = efeitos;
            InformacaoNutricional = informacaoNutricional;
        }

        public Guid CategoriaId { get; protected set; }
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        public string Composicao { get; protected set; }
        public int QuantidadeDisponivel { get; protected set; }
        public decimal Preco { get; protected set; }
        public Dimensoes Dimensoes { get; protected set; }
        public List<string> Imagens { get; protected set; }
        public List<Guid> Efeitos { get; protected set; }
        public InformacaoNutricional InformacaoNutricional { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new AdicionarProdutoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarProdutoCommandValidation : AbstractValidator<AdicionarProdutoCommand>
    {
        public AdicionarProdutoCommandValidation()
        {
            RuleFor(p => p.CategoriaId)
                .NotEqual(Guid.Empty)
                .WithMessage("O Id da categoria do produto não foi informado");

            RuleFor(p => p.Nome)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(p => p.Descricao)
                .NotEmpty()
                .WithMessage("A descrição do produto não foi informada");

            RuleFor(p => p.Composicao)
                .NotEmpty()
                .WithMessage("A composição do produto não foi informada");

            RuleFor(p => p.QuantidadeDisponivel)
                .GreaterThan(-1)
                .WithMessage("A quantidade do produto não pode ser negativa");

            RuleFor(p => p.Preco)
                .GreaterThan(0)
                .WithMessage("O preço do produto deve ser maior que zero");

            RuleFor(p => p.Dimensoes.Altura)
                .GreaterThan(0)
                .WithMessage("A altura do produto deve ser maior que zero");

            RuleFor(p => p.Dimensoes.Largura)
                .GreaterThan(0)
                .WithMessage("A largura do produto deve ser maior que zero");

            RuleFor(p => p.Dimensoes.Profundidade)
                .GreaterThan(0)
                .WithMessage("A profundidade do produto deve ser maior que zero");

            RuleFor(p => p.Imagens.Count)
                .GreaterThan(0)
                .WithMessage("O produto deve ao menos possuir uma imagem");

            RuleFor(p => p.Efeitos.Count)
                .GreaterThan(0)
                .WithMessage("O produto deve ao menos ter vínculo com um efeito");

            RuleFor(p => p.InformacaoNutricional.Cabecalho)
                .NotEmpty()
                .WithMessage("O cabeçalho da informação nutricional não foi informado");

            RuleFor(p => p.InformacaoNutricional.Legenda)
                .NotEmpty()
                .WithMessage("A legenda da informação nutricional não foi informado");

            RuleFor(p => p.InformacaoNutricional.CompostosNutricionais.Count)
                .GreaterThan(0)
                .WithMessage("A informação nutricional deve ao menos possuir um composto nutricional");
        }
    }
}
