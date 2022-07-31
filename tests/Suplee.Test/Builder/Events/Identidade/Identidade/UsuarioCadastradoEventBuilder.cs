using Suplee.Identidade.Domain.Identidade.Events;
using Suplee.Identidade.Domain.Models;
using Suplee.Test.Builder.Models;

namespace Suplee.Test.Builder.Events.Identidade.Identidade
{
    public class UsuarioCadastradoEventBuilder : UsuarioCadastradoEvent
    {
        public UsuarioCadastradoEventBuilder(Usuario usuario) : base(usuario)
        {
            Usuario = usuario;
        }

        public UsuarioCadastradoEvent Build() => this;
    }
}
