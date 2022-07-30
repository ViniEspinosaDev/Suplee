using Microsoft.EntityFrameworkCore;
using Suplee.Core.Data;
using Suplee.Identidade.Domain.Models;
using System.Threading.Tasks;

namespace Suplee.Identidade.Data
{
    public class IdentidadeContext : DbContext, IUnitOfWork
    {
        public IdentidadeContext(DbContextOptions<IdentidadeContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<ConfirmacaoUsuario> ConfirmacaoUsuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentidadeContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
