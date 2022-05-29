using System;

namespace Suplee.Catalogo.Api.Controllers.ViewModels
{
    public class ProdutoResumidoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string NomeCategoria { get; set; }
        public string NomeEfeito { get; set; }
        public string Imagem { get; set; }
    }
}
