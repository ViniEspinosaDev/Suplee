using Suplee.Core.Data;
using Suplee.Identidade.Domain.Models;
using System;

namespace Suplee.Identidade.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        bool ExisteUsuarioComCPF(string cpf);
        bool ExisteUsuarioComEmail(string email);

        ConfirmacaoUsuario ObterConfirmacaoUsuario(Guid usuarioId, string codigoConfirmacao);
        ConfirmacaoUsuario ObterConfirmacaoUsuario(Guid usuarioId);

        Usuario ObterPeloId(Guid usuarioId);
        Usuario ObterPeloCPF(string CPF);
        Usuario RealizarLoginEmail(string email, string senha);
        Usuario RealizarLoginCPF(string cpf, string senha);

        void Adicionar(Usuario usuario);
        void AdicionarConfirmacaoUsuario(ConfirmacaoUsuario confirmacaoUsuario);
    }
}
