using Suplee.Core.DomainObjects;
using Suplee.Core.Tools;
using Suplee.Identidade.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Suplee.Identidade.Domain.Models
{
    public class Usuario : Entity, IAggregateRoot
    {
        public Usuario(string nome, string email, string senha, string cPF, string celular, ETipoUsuario tipo, EStatusUsuario status)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            CPF = cPF.FormatarCPFApenasNumeros();
            Celular = celular;
            Tipo = tipo;
            Status = status;
            DataCadastro = DateTime.Now;
        }

        public string Nome { get; protected set; }
        public string Email { get; protected set; }
        public string Senha { get; protected set; }
        public string CPF { get; protected set; }
        public string Celular { get; protected set; }
        public ETipoUsuario Tipo { get; protected set; }
        public EStatusUsuario Status { get; protected set; }
        public DateTime DataCadastro { get; protected set; }

        public List<Endereco> Enderecos { get; protected set; }

        public void AdicionarEndereco(Endereco endereco)
        {
            if (Enderecos is null)
                Enderecos = new List<Endereco>();

            Enderecos.Add(endereco);
        }

        public void Ativar() => Status = EStatusUsuario.Ativo;

        public void AlterarSenha(string senha) => Senha = senha;

        public void Atualizar(string nome, string celular, List<Endereco> enderecos)
        {
            Nome = nome;
            Celular = celular;
            Enderecos = enderecos;
        }
    }
}
