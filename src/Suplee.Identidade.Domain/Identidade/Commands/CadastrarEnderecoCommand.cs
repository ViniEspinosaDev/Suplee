using FluentValidation;
using Suplee.Core.Messages;
using Suplee.Identidade.Domain.Enums;
using System;

namespace Suplee.Identidade.Domain.Identidade.Commands
{
    public class CadastrarEnderecoCommand : Command<bool>
    {
        public CadastrarEnderecoCommand(
            string nomeDestinatario,
            string cEP,
            string estado,
            string cidade,
            string bairro,
            string rua,
            string numero,
            string complemento,
            ETipoLocal tipoLocal,
            string telefone,
            string informacaoAdicional,
            bool enderecoPadrao)
        {
            NomeDestinatario = nomeDestinatario;
            CEP = cEP;
            Estado = estado;
            Cidade = cidade;
            Bairro = bairro;
            Rua = rua;
            Numero = numero;
            Complemento = complemento;
            TipoLocal = tipoLocal;
            Telefone = telefone;
            InformacaoAdicional = informacaoAdicional;
            EnderecoPadrao = enderecoPadrao;
        }

        public Guid UsuarioId { get; protected set; }
        public string NomeDestinatario { get; protected set; }
        public string CEP { get; protected set; }
        public string Estado { get; protected set; }
        public string Cidade { get; protected set; }
        public string Bairro { get; protected set; }
        public string Rua { get; protected set; }
        public string Numero { get; protected set; }
        public string Complemento { get; protected set; }
        public ETipoLocal TipoLocal { get; protected set; }
        public string Telefone { get; protected set; }
        public string InformacaoAdicional { get; protected set; }
        public bool EnderecoPadrao { get; protected set; }

        public void VincularUsuarioId(Guid usuarioId) => UsuarioId = usuarioId;

        public override bool IsValid()
        {
            ValidationResult = new CadastrarEnderecoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CadastrarEnderecoCommandValidation : AbstractValidator<CadastrarEnderecoCommand>
    {
        public CadastrarEnderecoCommandValidation()
        {
            RuleFor(x => x.UsuarioId)
               .NotEqual(Guid.Empty).WithMessage("O Id do usuário não foi informado")
               .OverridePropertyName("ValidacaoUsuarioId");

            RuleFor(x => x.NomeDestinatario)
               .NotEmpty().WithMessage("O Nome do destinatário não foi informado")
               .OverridePropertyName("ValidacaoNomeDestinatario");

            RuleFor(x => x.CEP)
               .NotEmpty().WithMessage("O CEP não foi informado")
               .OverridePropertyName("ValidacaoCEP");

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("O Estado não foi informado")
                .OverridePropertyName("ValidacaoEstado");

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("A Cidade não foi informada")
                .OverridePropertyName("ValidacaoCidade");

            RuleFor(x => x.Bairro)
               .NotEmpty().WithMessage("O Bairro não foi informado")
               .OverridePropertyName("ValidacaoBairro");

            RuleFor(x => x.Rua)
                .NotEmpty().WithMessage("A Rua não foi informada")
                .OverridePropertyName("ValidacaoRua");

            RuleFor(x => x.Numero)
               .NotEmpty().WithMessage("O Número não foi informado")
               .OverridePropertyName("ValidacaoNumero");

            RuleFor(x => x.TipoLocal)
               .NotNull().WithMessage("O Tipo do local não foi informado")
               .OverridePropertyName("ValidacaoTipoLocal");
        }
    }
}
