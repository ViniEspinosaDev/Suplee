using System;

namespace Suplee.Api.Controllers.Vendas.InputModels
{
    /// <summary>
    /// Realizar pagamento
    /// </summary>
    public class RealizarPagamentoInputModel
    {
        /// <summary>
        /// Usuário id (recuperado pelo token)
        /// </summary>
        public Guid? UsuarioId { get; set; }
        /// <summary>
        /// Sucesso
        /// </summary>
        public bool Sucesso { get; set; }
    }
}
