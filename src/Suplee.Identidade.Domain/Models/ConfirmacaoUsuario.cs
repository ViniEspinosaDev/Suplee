using Suplee.Core.DomainObjects;
using System;

namespace Suplee.Identidade.Domain.Models
{
    public class ConfirmacaoUsuario : Entity
    {
        public ConfirmacaoUsuario(Guid usuarioId, string codigoConfirmacao)
        {
            UsuarioId = usuarioId;
            CodigoConfirmacao = codigoConfirmacao;
            DataConfirmacao = null;
        }

        public Guid UsuarioId { get; protected set; }
        public string CodigoConfirmacao { get; protected set; }
        public DateTime? DataConfirmacao { get; protected set; }

        public bool Confirmado() => DataConfirmacao != null;

        public void Confirmar()
        {
            DataConfirmacao = DateTime.Now;
        }
    }
}
