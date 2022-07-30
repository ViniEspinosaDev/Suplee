using Suplee.Core.Messages.CommonMessages.DomainEvents;
using Suplee.Identidade.Domain.Models;

namespace Suplee.Identidade.Domain.Identidade.Events
{
    public class UsuarioCadastradoEvent : DomainEvent
    {
        public UsuarioCadastradoEvent(Usuario usuario) : base(usuario.Id)
        {
            Usuario = usuario;
        }

        public Usuario Usuario { get; protected set; }
    }
}
