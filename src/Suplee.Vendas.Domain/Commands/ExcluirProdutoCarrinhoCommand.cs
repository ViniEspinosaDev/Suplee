using FluentValidation;
using Suplee.Core.Messages;
using System;

namespace Suplee.Vendas.Domain.Commands
{
    public class ExcluirProdutoCarrinhoCommand : Command<bool>
    {
        public ExcluirProdutoCarrinhoCommand(Guid usuarioId, Guid produtoId)
        {
            UsuarioId = usuarioId;
            ProdutoId = produtoId;
        }

        public Guid UsuarioId { get; protected set; }
        public Guid ProdutoId { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new ExcluirProdutoCarrinhoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ExcluirProdutoCarrinhoCommandValidation : AbstractValidator<ExcluirProdutoCarrinhoCommand>
    {
        public ExcluirProdutoCarrinhoCommandValidation()
        {
            RuleFor(c => c.UsuarioId)
                .NotEqual(Guid.Empty).WithMessage("O Id do usuário não foi informado")
                .OverridePropertyName("ValidacaoUsuarioId");

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty).WithMessage("O Id do produto não foi informado")
                .OverridePropertyName("ValidacaoProdutoId");
        }
    }
}
