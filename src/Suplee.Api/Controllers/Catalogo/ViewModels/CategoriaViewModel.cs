using System;

namespace Suplee.Api.Controllers.Catalogo.ViewModels
{
    /// <summary>
    /// Categoria view model
    /// </summary>
    public class CategoriaViewModel
    {
        /// <summary>
        /// Id da categoria
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome da categoria
        /// </summary>
        public string Nome { get; set; }
    }
}