using Suplee.Identidade.Domain.Enums;
using System;

namespace Suplee.Api.Controllers.Identidade.ViewModels
{
    /// <summary>
    /// Retorna info do Usuário
    /// </summary>
    public class UsuarioViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Tipo do usuário
        /// </summary>
        public ETipoUsuario TipoUsuario { get; set; }
    }
}
