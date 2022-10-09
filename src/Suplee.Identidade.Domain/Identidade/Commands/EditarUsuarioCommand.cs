using FluentValidation;
using Suplee.Core.Messages;
using Suplee.Identidade.Domain.Models;
using System;
using System.Collections.Generic;

namespace Suplee.Identidade.Domain.Identidade.Commands
{
    public class EditarUsuarioCommand : Command<bool>
    {
        public EditarUsuarioCommand(Guid usuarioId, string nome, string celular, List<Endereco> enderecos)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Celular = celular;
            Enderecos = enderecos ?? new List<Endereco>();
        }

        public Guid UsuarioId { get; protected set; }
        public string Nome { get; protected set; }
        public string Celular { get; protected set; }
        public List<Endereco> Enderecos { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new EditarUsuarioCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class EditarUsuarioCommandValidation : AbstractValidator<EditarUsuarioCommand>
    {
        public EditarUsuarioCommandValidation()
        {
            RuleFor(x => x.UsuarioId)
               .NotEqual(Guid.Empty).WithMessage("O Id do usuário não foi informado")
               .OverridePropertyName("ValidacaoUsuarioId");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do usuário não foi informado")
                .MinimumLength(3).WithMessage("O nome do usuário deve ter ao mínimo 3 caracteres")
                .OverridePropertyName("ValidacaoNome");

            RuleFor(x => x.Enderecos.Count)
                .GreaterThan(0).WithMessage("Deve ter ao menos um endereço")
                .OverridePropertyName("ValidacaoEndereco");

            RuleForEach(x => x.Enderecos).ChildRules(Endereco =>
                {
                    Endereco.RuleFor(x => x.NomeDestinatario)
                       .NotEmpty().WithMessage("O Nome do destinatário não foi informado")
                       .OverridePropertyName("ValidacaoNomeDestinatario");

                    Endereco.RuleFor(x => x.CEP)
                       .NotEmpty().WithMessage("O CEP não foi informado")
                       .OverridePropertyName("ValidacaoCEP");

                    Endereco.RuleFor(x => x.Estado)
                       .NotEmpty().WithMessage("O Estado não foi informado")
                       .OverridePropertyName("ValidacaoEstado");

                    Endereco.RuleFor(x => x.Cidade)
                       .NotEmpty().WithMessage("A Cidade não foi informada")
                       .OverridePropertyName("ValidacaoCidade");

                    Endereco.RuleFor(x => x.Bairro)
                       .NotEmpty().WithMessage("O Bairro não foi informado")
                       .OverridePropertyName("ValidacaoBairro");

                    Endereco.RuleFor(x => x.Rua)
                       .NotEmpty().WithMessage("A Rua não foi informada")
                       .OverridePropertyName("ValidacaoRua");

                    Endereco.RuleFor(x => x.Numero)
                       .NotEmpty().WithMessage("O Número não foi informado")
                       .OverridePropertyName("ValidacaoNumero");

                    Endereco.RuleFor(x => x.TipoLocal)
                       .NotNull().WithMessage("O Tipo do local não foi informado")
                       .OverridePropertyName("ValidacaoTipoLocal");
                });
        }
    }
}
