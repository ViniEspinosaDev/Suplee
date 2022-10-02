using Suplee.Catalogo.Domain.DTO;

namespace Suplee.Catalogo.Domain.Interfaces.Repositories
{
    public interface IProdutoLeituraRepository
    {
        bool AdicionarProduto(ProdutoDTO produto);
    }
}
