using System;
using System.Collections.Generic;

namespace Suplee.Api.Controllers.Catalogo.ViewModels
{
    public class ProdutoResumidoViewModel
    {
        public ProdutoResumidoViewModel()
        {
            NomeEfeito = new List<string>();
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string NomeCategoria { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public List<string> NomeEfeito { get; set; }
        public List<ProdutoImagemViewModel> Imagens { get; set; }
    }
}
