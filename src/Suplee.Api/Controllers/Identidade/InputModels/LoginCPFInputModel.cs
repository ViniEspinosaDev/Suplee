namespace Suplee.Api.Controllers.Identidade.InputModels
{
    /// <summary>
    /// Template Login
    /// </summary>
    public class LoginCPFInputModel
    {
        /// <summary>
        /// E-mail
        /// </summary>
        public string CPF { get; set; }
        /// <summary>
        /// Senha
        /// </summary>
        public string Senha { get; set; }
    }
}
