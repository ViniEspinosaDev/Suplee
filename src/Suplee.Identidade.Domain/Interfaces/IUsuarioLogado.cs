using Suplee.Identidade.Domain.Enums;
using System;

namespace Suplee.Identidade.Domain.Interfaces
{
    public interface IUsuarioLogado
    {
        Guid UsuarioId { get; }
        string Nome { get; }
        string Email { get; }
        ETipoUsuario TipoUsuario { get; }
        bool EstaAutenticado();
    }
}
