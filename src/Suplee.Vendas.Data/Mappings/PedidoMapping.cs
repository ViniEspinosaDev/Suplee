using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Suplee.Vendas.Domain.Models;

namespace Suplee.Vendas.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido");

            builder.HasKey("Id");

            builder.Property(c => c.Codigo)
                .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            builder
                .HasMany(p => p.Produtos)
                .WithOne(p => p.Pedido)
                .HasForeignKey(p => p.PedidoId);
        }
    }
}
