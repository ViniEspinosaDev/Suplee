using FluentValidation;
using Suplee.Core.Messages;
using System;

namespace Suplee.Vendas.Domain.Commands
{
    public class AtualizarProdutoCarrinhoCommand : Command<bool>
    {
        public AtualizarProdutoCarrinhoCommand(Guid produtoId, int quantidade)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }

        public Guid UsuarioId { get; protected set; }
        public Guid ProdutoId { get; protected set; }
        public int Quantidade { get; protected set; }

        public void VincularUsuarioId(Guid usuarioId) => UsuarioId = usuarioId;

        public override bool IsValid()
        {
            ValidationResult = new AtualizarProdutoCarrinhoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AtualizarProdutoCarrinhoCommandValidation : AbstractValidator<AtualizarProdutoCarrinhoCommand>
    {
        public AtualizarProdutoCarrinhoCommandValidation()
        {
            RuleFor(c => c.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O Id do usuário não foi informado")
                .OverridePropertyName("ValidacaoUsuarioId");

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty).WithMessage("O Id do produto não foi informado")
                .OverridePropertyName("ValidacaoProdutoId");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0).WithMessage("A quantidade deve ser superior a zero")
                .OverridePropertyName("ValidacaoQuantidade");
        }
    }
}
