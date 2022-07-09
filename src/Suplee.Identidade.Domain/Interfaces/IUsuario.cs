using Suplee.Identidade.Domain.Enums;
using System;

namespace Suplee.Identidade.Domain.Interfaces
{
    public interface IUsuario
    {
        Guid UsuarioId { get; }
        string Nome { get; }
        string Email { get; }
        ETipoUsuario TipoUsuario { get; }
        bool EstaAutenticado();
    }
}
