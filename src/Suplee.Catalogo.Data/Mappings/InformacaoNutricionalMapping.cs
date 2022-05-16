using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suplee.Catalogo.Domain.Models;

namespace Suplee.Catalogo.Data.Mappings
{
    public class InformacaoNutricionalMapping : IEntityTypeConfiguration<InformacaoNutricional>
    {
        public void Configure(EntityTypeBuilder<InformacaoNutricional> builder)
        {
            builder.ToTable("InformacaoNutricional");

            builder.HasKey(i => i.Id);

            builder.HasMany(i => i.CompostosNutricionais)
                .WithOne(i => i.InformacaoNutricional)
                .HasForeignKey(c => c.InformacaoNutricionalId);
        }
    }
}
