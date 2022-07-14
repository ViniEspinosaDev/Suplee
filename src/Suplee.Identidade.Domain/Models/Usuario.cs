using Suplee.Core.DomainObjects;
using Suplee.Identidade.Domain.Enums;
using System.Collections.Generic;

namespace Suplee.Identidade.Domain.Models
{
    public class Usuario : Entity, IAggregateRoot
    {
        public Usuario(string nome, string email, string senha, string cPF, string celular, ETipoUsuario tipoUsuario)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            CPF = cPF;
            Celular = celular;
            TipoUsuario = tipoUsuario;
        }

        public string Nome { get; protected set; }
        public string Email { get; protected set; }
        public string Senha { get; protected set; }
        public string CPF { get; protected set; }
        public string Celular { get; protected set; }
        public ETipoUsuario TipoUsuario { get; protected set; }

        public ICollection<Endereco> Enderecos { get; protected set; }

        public void AdicionarEndereco(Endereco endereco)
        {
            if (Enderecos is null)
                Enderecos = new List<Endereco>();

            Enderecos.Add(endereco);
        }
    }
}
