using Microsoft.EntityFrameworkCore;
using Suplee.Catalogo.Domain.Models;
using Suplee.Core.Data;
using System.Threading.Tasks;

namespace Suplee.Catalogo.Data
{
    public class CatalogoContext : DbContext, IUnitOfWork
    {
        public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Efeito> Efeitos { get; set; }
        public DbSet<ProdutoEfeito> ProdutosEfeitos { get; set; }
        public DbSet<ProdutoImagem> ProdutosImagens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
