namespace Suplee.Api.Controllers.Identidade.InputModels
{
    /// <summary>
    /// Template Login
    /// </summary>
    public class LoginInputModel
    {
        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Senha
        /// </summary>
        public string Senha { get; set; }
    }
}
