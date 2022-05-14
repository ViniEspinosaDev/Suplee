using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suplee.Catalogo.Domain.Models;

namespace Suplee.Catalogo.Data.Mappings
{
    public class EfeitoMapping : IEntityTypeConfiguration<Efeito>
    {
        public void Configure(EntityTypeBuilder<Efeito> builder)
        {
            builder.ToTable("Efeito");

            builder.HasKey(e => e.Id);
        }
    }
}
