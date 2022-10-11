using Suplee.Core.Data;
using Suplee.Vendas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Suplee.Vendas.Domain.Interfaces
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<Pedido> ObterPorId(Guid pedidoId);
        Task<Pedido> ObterCarrinhoPorUsuarioId(Guid usuarioId);
        Task<List<Pedido>> ObterHistoricoPedido(Guid usuarioId);
        Task<bool> ExistePedidoIniciadoParaUsuario(Guid usuarioId);
        void Adicionar(Pedido pedido);
        void AdicionarPedidoProduto(PedidoProduto pedidoProduto);
        void AtualizarPedidoProduto(PedidoProduto pedidoProduto);
    }
}
