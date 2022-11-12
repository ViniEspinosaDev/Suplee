using Suplee.Catalogo.Domain.DTO;
using System;
using System.Collections.Generic;

namespace Suplee.Catalogo.Domain.Interfaces.Repositories
{
    public interface IProdutoLeituraRepository
    {
        void AdicionarProduto(ProdutoDTO produto);
        void AtualizarProduto(ProdutoDTO produto);

        ProdutoDTO RecuperarProduto(Guid produtoId);
        List<ProdutoDTO> RecuperarProdutosComEstoque();
    }
}
