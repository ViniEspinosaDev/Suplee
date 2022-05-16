using FluentValidation;
using Suplee.Catalogo.Domain.Models;
using Suplee.Catalogo.Domain.ValueObjects;
using Suplee.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Suplee.Catalogo.Domain.Commands
{
    public class AtualizarProdutoCommand : Command
    {
        public AtualizarProdutoCommand(
            Guid produtoId,
            string nome,
            string descricao,
            string composicao,
            int quantidadeDisponivel,
            decimal preco,
            Dimensoes dimensoes,
            Guid categoriaId,
            List<ProdutoImagem> imagens,
            List<ProdutoEfeito> efeitos,
            InformacaoNutricional informacaoNutricional)
        {
            ProdutoId = produtoId;
            Nome = nome;
            Descricao = descricao;
            Composicao = composicao;
            QuantidadeDisponivel = quantidadeDisponivel;
            Preco = preco;
            Dimensoes = dimensoes;
            CategoriaId = categoriaId;
            Imagens = imagens;
            Efeitos = efeitos;
            InformacaoNutricional = informacaoNutricional;
        }

        public Guid ProdutoId { get; protected set; }
        public string Nome { get; protected set; }
        public string Descricao { get; protected set; }
        public string Composicao { get; protected set; }
        public int QuantidadeDisponivel { get; protected set; }
        public decimal Preco { get; protected set; }
        public Dimensoes Dimensoes { get; protected set; }
        public Guid CategoriaId { get; protected set; }
        public List<ProdutoImagem> Imagens { get; protected set; }
        public List<ProdutoEfeito> Efeitos { get; protected set; }
        public InformacaoNutricional InformacaoNutricional { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new AtualizarProdutoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class AtualizarProdutoCommandValidation : AbstractValidator<AtualizarProdutoCommand>
        {
            public AtualizarProdutoCommandValidation()
            {
                RuleFor(p => p.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("O Id do produto não foi informado");

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

                RuleFor(p => p.Imagens.Any(x => x.Id == Guid.Empty))
                    .NotEmpty()
                    .WithMessage("Todos Ids das imagens devem estar preenchidos");

                RuleFor(p => p.Efeitos.Count)
                    .GreaterThan(0)
                    .WithMessage("O produto deve ao menos ter vínculo com um efeito");

                RuleFor(p => p.Efeitos.Any(x => x.Id == Guid.Empty))
                    .NotEmpty()
                    .WithMessage("Todos Ids dos efeitos devem estar preenchidos");

                RuleFor(p => p.InformacaoNutricional.Id)
                    .NotEqual(Guid.Empty)
                    .WithMessage("O Id da informação nutricional não foi informado");

                RuleFor(p => p.InformacaoNutricional.Cabecalho)
                    .NotEmpty()
                    .WithMessage("O cabeçalho da informação nutricional não foi informado");

                RuleFor(p => p.InformacaoNutricional.Legenda)
                    .NotEmpty()
                    .WithMessage("A legenda da informação nutricional não foi informado");

                RuleFor(p => p.InformacaoNutricional.CompostosNutricionais.Count)
                    .GreaterThan(0)
                    .WithMessage("A informação nutricional deve ao menos possuir um composto nutricional");

                RuleFor(p => p.InformacaoNutricional.CompostosNutricionais.Any(x => x.Id == Guid.Empty))
                    .NotEmpty()
                    .WithMessage("Todos Ids dos compostos nutricionais devem estar preenchidos");
            }
        }
    }
}
