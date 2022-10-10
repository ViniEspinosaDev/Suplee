using Microsoft.EntityFrameworkCore;
using Suplee.Core.Data;
using Suplee.Vendas.Domain.Models;
using System.Threading.Tasks;

namespace Suplee.Vendas.Data
{
    public class VendasContext : DbContext, IUnitOfWork
    {
        public VendasContext(DbContextOptions<VendasContext> options) : base(options)
        {
        }

        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoProduto> PedidoProduto { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<ProdutoImagem> ProdutoImagem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VendasContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
