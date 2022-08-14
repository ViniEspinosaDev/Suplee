using FluentValidation;
using Suplee.Core.Messages;
using System;

namespace Suplee.Vendas.Domain.Commands
{
    public class InserirProdutoCarrinhoCommand : Command<bool>
    {
        public InserirProdutoCarrinhoCommand(Guid usuarioId, Guid produtoId, string nomeProduto, int quantidade, decimal valorUnitario)
        {
            UsuarioId = usuarioId;
            ProdutoId = produtoId;
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public Guid UsuarioId { get; protected set; }
        public Guid ProdutoId { get; protected set; }
        public string NomeProduto { get; protected set; }
        public int Quantidade { get; protected set; }
        public decimal ValorUnitario { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new InserirProdutoCarrinhoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class InserirProdutoCarrinhoCommandValidation : AbstractValidator<InserirProdutoCarrinhoCommand>
    {
        public InserirProdutoCarrinhoCommandValidation()
        {
            RuleFor(c => c.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O Id do usuário não foi informado")
                .OverridePropertyName("ValidacaoUsuarioId");

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty).WithMessage("O Id do produto não foi informado")
                .OverridePropertyName("ValidacaoProdutoId");

            RuleFor(c => c.NomeProduto)
                .NotEmpty().WithMessage("O Nome do produto não foi informado")
                .OverridePropertyName("ValidacaoNomeProduto");

            RuleFor(c => c.Quantidade)
                .GreaterThanOrEqualTo(default(int)).WithMessage("A Quantidade do produto deve ser nula ou positiva")
                .OverridePropertyName("ValidacaoQuantidade");

            RuleFor(c => c.ValorUnitario)
                .GreaterThan(default(decimal)).WithMessage("O Valor unitário deve ser maior que zero")
                .OverridePropertyName("ValidacaoValorUnitario");
        }
    }
}
