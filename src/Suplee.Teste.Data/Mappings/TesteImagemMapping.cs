using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suplee.Teste.Domain.Models;

namespace Suplee.Teste.Data.Mappings
{
    public class TesteImagemMapping : IEntityTypeConfiguration<TesteImagem>
    {
        public void Configure(EntityTypeBuilder<TesteImagem> builder)
        {
            builder.ToTable("TesteImagem");

            builder.HasKey(p => p.Id);
        }
    }
}
