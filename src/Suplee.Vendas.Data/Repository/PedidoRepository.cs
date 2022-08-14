using Microsoft.EntityFrameworkCore;
using Suplee.Core.Data;
using Suplee.Vendas.Domain.Enums;
using Suplee.Vendas.Domain.Interfaces;
using Suplee.Vendas.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Suplee.Vendas.Data.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly VendasContext _vendasContext;

        public PedidoRepository(VendasContext vendasContext)
        {
            _vendasContext = vendasContext;
        }

        public IUnitOfWork UnitOfWork => _vendasContext;

        public void Dispose() => _vendasContext?.Dispose();

        public void Adicionar(Pedido pedido) => _vendasContext.Pedido.Add(pedido);

        public async Task<Pedido> ObterPedidoPorUsuarioId(Guid usuarioId) =>
            await _vendasContext.Pedido
                .Include(x => x.Produtos)
                .FirstOrDefaultAsync(x => x.UsuarioId == usuarioId
                    && x.Status != EPedidoStatus.Cancelado
                    && x.Status != EPedidoStatus.Pago
                    && x.Status != EPedidoStatus.Enviado
                    && x.Status != EPedidoStatus.Entregue);

        public async Task<Pedido> ObterPorId(Guid pedidoId) =>
            await _vendasContext.Pedido
                .Include(x => x.Produtos)
                .FirstOrDefaultAsync(x => x.Id == pedidoId);

        public async Task<bool> ExistePedidoIniciadoParaUsuario(Guid usuarioId) =>
            await _vendasContext.Pedido
                .FirstOrDefaultAsync(x => x.UsuarioId == usuarioId && x.Status == EPedidoStatus.Iniciado) != null;

        public void AdicionarPedidoProduto(PedidoProduto pedidoProduto) => _vendasContext.PedidoProduto.Add(pedidoProduto);

        public void AtualizarPedidoProduto(PedidoProduto pedidoProduto) => _vendasContext.PedidoProduto.Update(pedidoProduto);
    }
}
