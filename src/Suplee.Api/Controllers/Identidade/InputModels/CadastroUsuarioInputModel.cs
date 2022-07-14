namespace Suplee.Api.Controllers.Identidade.InputModel
{
    /// <summary>
    /// Nova conta
    /// </summary>
    public class CadastroUsuarioInputModel
    {
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// CPF
        /// </summary>
        public string CPF { get; set; }
        /// <summary>
        /// Celular
        /// </summary>
        public string Celular { get; set; }
        /// <summary>
        /// Senha
        /// </summary>
        public string Senha { get; set; }
        /// <summary>
        /// Confirmação da senha
        /// </summary>
        public string ConfirmacaoSenha { get; set; }
    }
}
