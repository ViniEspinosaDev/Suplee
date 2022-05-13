using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suplee.Catalogo.Domain.Models;

namespace Suplee.Catalogo.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(p => p.Id);

            builder.OwnsOne(c => c.Dimensoes, cm =>
            {
                cm.Property(c => c.Altura)
                    .HasColumnName("Altura");

                cm.Property(c => c.Largura)
                    .HasColumnName("Largura");

                cm.Property(c => c.Profundidade)
                    .HasColumnName("Profundidade");
            });

            builder.HasOne(p => p.Categoria)
                .WithMany()
                .HasForeignKey(p => p.CategoriaId);

            //builder.HasMany(p => p.Efeitos)
            //    .WithOne()
            //    .HasForeignKey(p => p.)

            // TODO: Fazer o mapping certo das imagens e dos efeitos do Produto, depois dar continuidade nos endpoints, api, events, commands, etc...
        }
    }
}
