using System;

namespace Suplee.Api.Controllers.Catalogo.ViewModels
{
    /// <summary>
    /// Frete
    /// </summary>
    public class FreteViewModel
    {
        /// <summary>
        /// Preço
        /// </summary>
        public decimal Preco { get; set; }
        /// <summary>
        /// Numero de dias
        /// </summary>
        public int PrazoDias { get; set; }
        /// <summary>
        /// Data estimada
        /// </summary>
        public DateTime DataEstimada { get; set; }
    }
}
