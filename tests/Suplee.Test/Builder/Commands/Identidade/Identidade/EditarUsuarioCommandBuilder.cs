using Suplee.Identidade.Domain.Identidade.Commands;
using Suplee.Identidade.Domain.Models;
using Suplee.Test.Builder.Models;
using System;
using System.Collections.Generic;

namespace Suplee.Test.Builder.Commands.Identidade.Identidade
{
    public class EditarUsuarioCommandBuilder : EditarUsuarioCommand
    {
        public EditarUsuarioCommandBuilder(
            Guid usuarioId = default,
            string nome = default,
            string celular = default,
            List<Endereco> enderecos = default) : base(usuarioId, nome, celular, enderecos)
        {
        }

        public EditarUsuarioCommandBuilder ComandoValido()
        {
            UsuarioId = Guid.NewGuid();
            Nome = "Nome";
            Celular = "15997882266";
            Enderecos = new List<Endereco>() { new EnderecoBuilder().PadraoValido().Build() };

            return this;
        }

        public EditarUsuarioCommandBuilder ComandoInvalido()
        {
            UsuarioId = Guid.Empty;
            Nome = "";
            Celular = "";
            Enderecos = new List<Endereco>() { new EnderecoBuilder().PadraoInvalido().Build() };

            return this;
        }

        public EditarUsuarioCommand Build() => this;
    }
}
