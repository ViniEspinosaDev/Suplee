using Suplee.Core.Data;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Identidade.Domain.Models;
using System;
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

        public void Adicionar(Usuario usuario) => _identidadeContext.Add(usuario);

        public bool ExisteUsuarioComCPF(string cpf) => _identidadeContext.Usuario.Any(x => x.CPF == cpf);

        public bool ExisteUsuarioComEmail(string email) => _identidadeContext.Usuario.Any(x => x.Email == email);

        public Usuario ObterPeloId(Guid usuarioId) => _identidadeContext.Usuario.FirstOrDefault(x => x.Id == usuarioId);

        public Usuario RealizarLoginEmail(string email, string senha) =>
            _identidadeContext.Usuario.FirstOrDefault(x => x.Email == email && x.Senha == senha);

        public Usuario RealizarLoginCPF(string cpf, string senha) =>
            _identidadeContext.Usuario.FirstOrDefault(x => x.CPF == cpf && x.Senha == senha);

        public ConfirmacaoUsuario ObterConfirmacaoUsuario(Guid usuarioId, string codigoConfirmacao) =>
            _identidadeContext.ConfirmacaoUsuario.FirstOrDefault(x => x.UsuarioId == usuarioId && x.CodigoConfirmacao == codigoConfirmacao);

        public ConfirmacaoUsuario ObterConfirmacaoUsuario(Guid usuarioId) =>
            _identidadeContext.ConfirmacaoUsuario.FirstOrDefault(x => x.UsuarioId == usuarioId && x.DataConfirmacao == null);

        public Usuario ObterPeloCPF(string CPF) => _identidadeContext.Usuario.FirstOrDefault(x => x.CPF == CPF);

        public void AdicionarConfirmacaoUsuario(ConfirmacaoUsuario confirmacaoUsuario) => _identidadeContext.ConfirmacaoUsuario.Add(confirmacaoUsuario);

        public void Dispose() => _identidadeContext?.Dispose();
    }
}
