using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suplee.Catalogo.Domain.Models;

namespace Suplee.Catalogo.Data.Mappings
{
    public class ProdutoImagemMapping : IEntityTypeConfiguration<ProdutoImagem>
    {
        public void Configure(EntityTypeBuilder<ProdutoImagem> builder)
        {
            builder.ToTable("ProdutoImagem");

            builder.HasKey(p => p.Id);
        }
    }
}
