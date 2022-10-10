using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suplee.Vendas.Domain.Models;

namespace Suplee.Vendas.Data.Mappings
{
    public class ProdutoImagemMapping : IEntityTypeConfiguration<ProdutoImagem>
    {
        public void Configure(EntityTypeBuilder<ProdutoImagem> builder)
        {
            builder.ToTable("ProdutoImagem");

            builder.HasKey(c => c.Id);
        }
    }
}
