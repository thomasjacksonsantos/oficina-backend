

using Oficina.Domain.Aggregates.StatusPedidoCompraAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Oficina.Infrastructure.DataAccess.Mappings;

public class EfStatusPedidoCompraConfiguration : IEntityTypeConfiguration<StatusPedidoCompra>
{
    public void Configure(EntityTypeBuilder<StatusPedidoCompra> builder)
    {
        builder.Property(c => c.Descricao).HasMaxLength(1000);
                
        builder.ComplexProperty(c => c.Criado, criado =>
        {
            criado.Property(c => c.Valor).HasColumnName(nameof(StatusPedidoCompra.Criado));
        });
        builder.ComplexProperty(c => c.Atualizado, atualizado =>
        {
            atualizado.Property(c => c.Valor).HasColumnName(nameof(StatusPedidoCompra.Atualizado));
        });

        builder.HasOne(c => c.StatusPedidoCompraStatus)
            .WithMany()
            .HasForeignKey(c => c.StatusPedidoCompraStatusId)
            .OnDelete(DeleteBehavior.NoAction); 
    }
}