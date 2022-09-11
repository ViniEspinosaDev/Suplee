using FluentValidation;
using Suplee.Core.Messages;
using System;
using System.Collections.Generic;

namespace Suplee.Vendas.Domain.Commands
{
    public class CadastrarCarrinhoCommand : Command<bool>
    {
        public CadastrarCarrinhoCommand(Guid usuarioId, List<CadastrarCarrinhoCommandProduto> produtos)
        {
            UsuarioId = usuarioId;
            Produtos = produtos;
        }

        public Guid UsuarioId { get; protected set; }
        public List<CadastrarCarrinhoCommandProduto> Produtos { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarCarrinhoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public class CadastrarCarrinhoCommandProduto
        {
            public CadastrarCarrinhoCommandProduto(Guid produtoId, string nomeProduto, int quantidade, decimal valorUnitario)
            {
                ProdutoId = produtoId;
                NomeProduto = nomeProduto;
                Quantidade = quantidade;
                ValorUnitario = valorUnitario;
            }

            public Guid ProdutoId { get; protected set; }
            public string NomeProduto { get; protected set; }
            public int Quantidade { get; protected set; }
            public decimal ValorUnitario { get; protected set; }
        }
    }

    public class CadastrarCarrinhoCommandValidation : AbstractValidator<CadastrarCarrinhoCommand>
    {
        public CadastrarCarrinhoCommandValidation()
        {
            RuleFor(c => c.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O Id do usuário não foi informado")
                .OverridePropertyName("ValidacaoUsuarioId");

            RuleFor(c => c.Produtos)
                .NotNull().WithMessage("Não há produtos para criação do carrinho")
                .OverridePropertyName("ValidacaoProdutos");

            RuleForEach(v => v.Produtos)
             .ChildRules(produto =>
             {
                 produto.RuleFor(x => x.ProdutoId)
                    .NotEqual(Guid.Empty).WithMessage("O Id do produto não foi informado")
                    .OverridePropertyName("ValidacaoProdutoId");

                 produto.RuleFor(x => x.NomeProduto)
                    .NotEmpty().WithMessage("O nome do produto não foi informado")
                    .OverridePropertyName("ValidacaoNomeProduto");

                 produto.RuleFor(x => x.Quantidade)
                    .GreaterThan(0).WithMessage("A quantidade não pode ser igual ou inferior a zero")
                    .OverridePropertyName("ValidacaoQuantidade");

                 produto.RuleFor(x => x.ValorUnitario)
                    .GreaterThan(0).WithMessage("O valor unitário não pode ser igual ou inferior a zero")
                    .OverridePropertyName("ValidacaoValorUnitario");
             });
        }
    }
}
