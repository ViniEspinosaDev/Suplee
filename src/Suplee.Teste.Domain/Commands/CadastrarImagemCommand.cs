using FluentValidation;
using Microsoft.AspNetCore.Http;
using Suplee.Core.Messages;

namespace Suplee.Teste.Domain.Commands
{
    public class CadastrarImagemCommand : Command<bool>
    {
        public CadastrarImagemCommand(IFormFile imagem)
        {
            Imagem = imagem;
        }

        public IFormFile Imagem { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new CadastrarImagemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CadastrarImagemCommandValidation : AbstractValidator<CadastrarImagemCommand>
    {
        public CadastrarImagemCommandValidation()
        {
            RuleFor(c => c.Imagem)
                .NotNull().WithMessage("A imagem não foi informada");
        }
    }
}
