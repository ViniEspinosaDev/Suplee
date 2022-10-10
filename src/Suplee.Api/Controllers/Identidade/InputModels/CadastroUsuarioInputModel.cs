namespace Suplee.Api.Controllers.Identidade.InputModel
{
    public class CadastroUsuarioInputModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Celular { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
    }
}
