namespace Suplee.Api.Controllers.Identidade.ViewModels
{
    /// <summary>
    /// Informações de login
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Token de acesso
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Expiração em minutos
        /// </summary>
        public double ExpiresIn { get; set; }
        /// <summary>
        /// Informacoes do token
        /// </summary>
        public UserTokenViewModel UserToken { get; set; }
    }

    /// <summary>
    /// Token de usuário
    /// </summary>
    public class UserTokenViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string UsuarioId { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Tipo de usuário
        /// </summary>
        public string TipoUsuario { get; set; }
    }

    /// <summary>
    /// Claim
    /// </summary>
    public class ClaimViewModel
    {
        /// <summary>
        /// Tipo
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Valor
        /// </summary>
        public string Value { get; set; }
    }
}
