using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suplee.Identidade.Domain.Models;

namespace Suplee.Identidade.Data.Mappings
{
    public class ConfirmacaoUsuarioMapping : IEntityTypeConfiguration<ConfirmacaoUsuario>
    {
        public void Configure(EntityTypeBuilder<ConfirmacaoUsuario> builder)
        {
            builder.ToTable("ConfirmacaoUsuario");
        }
    }
}
