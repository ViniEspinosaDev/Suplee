using Microsoft.EntityFrameworkCore;
using Suplee.Core.Data;
using Suplee.Teste.Domain.Models;
using System.Threading.Tasks;

namespace Suplee.Teste.Data
{
    public class TesteContext : DbContext, IUnitOfWork
    {
        public TesteContext(DbContextOptions<TesteContext> options) : base(options)
        {
        }

        public DbSet<TesteImagem> TesteImagem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TesteContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
