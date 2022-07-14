using Microsoft.AspNetCore.Http;
using Suplee.Identidade.Domain.Enums;
using Suplee.Identidade.Domain.Interfaces;
using System;
using System.Security.Claims;

namespace Suplee.Api.Extensions
{
    /// <summary>
    /// Recupera informações do token
    /// </summary>
    public class AspNetUser : IUsuarioLogado
    {
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="accessor"></param>
        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// Id do usuário
        /// </summary>
        public Guid UsuarioId => EstaAutenticado() ? Guid.Parse(_accessor.HttpContext.User.RecuperarIdUsuario()) : Guid.Empty;

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome => EstaAutenticado() ? _accessor.HttpContext.User.RecuperarNomeUsuario() : "";


        /// <summary>
        /// E-mail do usuário
        /// </summary>
        public string Email => EstaAutenticado() ? _accessor.HttpContext.User.RecuperarEmailUsuario() : "";


        /// <summary>
        /// Tipo do usuário (Normal, Adm, Robo)
        /// </summary>
        public ETipoUsuario TipoUsuario => EstaAutenticado() ? _accessor.HttpContext.User.RecuperarTipoUsuario() : ETipoUsuario.Nenhum;

        /// <summary>
        /// Verifica se usuário está logado
        /// </summary>
        /// <returns></returns>
        public bool EstaAutenticado()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }

    /// <summary>
    /// Extensões para Claim
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Recupera id do usuário do token
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string RecuperarIdUsuario(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypeExtension.UsuarioId);
            return claim?.Value;
        }

        /// <summary>
        /// Recupera nome do usuário do Token
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string RecuperarNomeUsuario(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypeExtension.Nome);
            return claim?.Value;
        }

        /// <summary>
        /// Recupera e-mail do usuário do Token
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string RecuperarEmailUsuario(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypeExtension.Email);
            return claim?.Value;
        }

        /// <summary>
        /// Recupera tipo do usuário do Token
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static ETipoUsuario RecuperarTipoUsuario(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypeExtension.TipoUsuario);

            if (string.IsNullOrEmpty(claim?.Value))
                return ETipoUsuario.Nenhum;

            Enum.TryParse(claim.Value, out ETipoUsuario tipoUsuario);

            return tipoUsuario;
        }
    }
}
