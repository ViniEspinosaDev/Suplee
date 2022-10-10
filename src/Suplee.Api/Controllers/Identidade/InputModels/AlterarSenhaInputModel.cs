using System;

namespace Suplee.Api.Controllers.Identidade.InputModels
{
    public class AlterarSenhaInputModel
    {
        public Guid UsuarioId { get; set; }
        public string CodigoConfirmacao { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
    }
}
