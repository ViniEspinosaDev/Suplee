using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suplee.Vendas.Domain.Models;

namespace Suplee.Vendas.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(c => c.Id);

            builder.HasMany(x => x.Imagens)
                .WithOne(x => x.Produto)
                .HasForeignKey(x => x.ProdutoId);
        }
    }
}
