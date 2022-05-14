using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suplee.Catalogo.Domain.Models;

namespace Suplee.Catalogo.Data.Mappings
{
    public class CompostoNutricionalMapping : IEntityTypeConfiguration<CompostoNutricional>
    {
        public void Configure(EntityTypeBuilder<CompostoNutricional> builder)
        {
            builder.ToTable("CompostoNutricional");

            builder.HasKey(c => c.Id);
        }
    }
}
