﻿namespace Suplee.Api.Controllers.Identidade.InputModel
{
    public class NovaContaInputModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
    }
}
