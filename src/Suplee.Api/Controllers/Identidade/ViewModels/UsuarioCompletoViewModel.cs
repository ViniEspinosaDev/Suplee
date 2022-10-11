using Suplee.Api.Controllers.Vendas.ViewModels;

namespace Suplee.Api.Controllers.Identidade.ViewModels
{
    public class UsuarioCompletoViewModel
    {
        public UsuarioViewModel Usuario { get; set; }
        public PedidoViewModel Carrinho { get; set; }
    }
}
