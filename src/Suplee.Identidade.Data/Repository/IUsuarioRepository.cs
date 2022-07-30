using Suplee.Core.Data;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Identidade.Domain.Models;
using System.Linq;

namespace Suplee.Identidade.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IdentidadeContext _identidadeContext;

        public UsuarioRepository(IdentidadeContext identidadeContext)
        {
            _identidadeContext = identidadeContext;
        }

        public IUnitOfWork UnitOfWork => _identidadeContext;

        public void Adicionar(Usuario usuario)
        {
            _identidadeContext.Add(usuario);
        }

        public bool ExisteUsuarioComCPF(string cpf)
        {
            return _identidadeContext.Usuario.Any(x => x.CPF == cpf);
        }

        public bool ExisteUsuarioComEmail(string email)
        {
            return _identidadeContext.Usuario.Any(x => x.Email == email);
        }

        public Usuario RealizarLoginEmail(string email, string senha)
        {
            return _identidadeContext.Usuario.FirstOrDefault(x => x.Email == email && x.Senha == senha);
        }

        public Usuario RealizarLoginCPF(string cpf, string senha)
        {
            return _identidadeContext.Usuario.FirstOrDefault(x => x.CPF == cpf && x.Senha == senha);
        }

        public void Dispose()
        {
            _identidadeContext?.Dispose();
        }
    }
}
