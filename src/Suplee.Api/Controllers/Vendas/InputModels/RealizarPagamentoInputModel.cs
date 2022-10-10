using System;

namespace Suplee.Api.Controllers.Vendas.InputModels
{
    public class RealizarPagamentoInputModel
    {
        public Guid? UsuarioId { get; set; }
        public bool Sucesso { get; set; }
    }
}
