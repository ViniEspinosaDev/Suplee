using System;

namespace Suplee.Api.Controllers.Identidade.InputModels
{
    /// <summary>
    /// Informações para alterar senha
    /// </summary>
    public class AlterarSenhaInputModel
    {
        /// <summary>
        /// Id do usuário
        /// </summary>
        public Guid UsuarioId { get; set; }
        /// <summary>
        /// Codigo de confirmação
        /// </summary>
        public string CodigoConfirmacao { get; set; }
        /// <summary>
        /// Senha
        /// </summary>
        public string Senha { get; set; }
        /// <summary>
        /// Confirmação senha
        /// </summary>
        public string ConfirmacaoSenha { get; set; }
    }
}
