using Suplee.Identidade.Domain.Enums;
using System;

namespace Suplee.Api.Controllers.Identidade.ViewModels
{
    public class UsuarioViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public ETipoUsuario TipoUsuario { get; set; }
    }
}
