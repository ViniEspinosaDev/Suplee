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

        public async Task<Pedido> ObterCarrinhoPorUsuarioId(Guid usuarioId) =>
            await _vendasContext.Pedido
                .Include(x => x.Produtos)
                    .ThenInclude(x => x.Produto)
                        .ThenInclude(x => x.Imagens)
                .FirstOrDefaultAsync(x => x.UsuarioId == usuarioId && x.Status == EPedidoStatus.Rascunho);

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
