using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suplee.Catalogo.Domain.Models;

namespace Suplee.Catalogo.Data.Mappings
{
    public class ProdutoEfeitoMapping : IEntityTypeConfiguration<ProdutoEfeito>
    {
        public void Configure(EntityTypeBuilder<ProdutoEfeito> builder)
        {
            builder.ToTable("ProdutoEfeito");

            builder.HasKey(p => p.Id);
        }
    }
}
