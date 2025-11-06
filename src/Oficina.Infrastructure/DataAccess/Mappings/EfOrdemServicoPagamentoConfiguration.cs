using Oficina.Domain.Aggregates.OrdemServicoAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarePath.Infrastructure.DataAccess.Mappings;

public sealed class EfOrdemServicoPagamentoConfiguration : IEntityTypeConfiguration<OrdemServicoPagamento>
{
    public void Configure(EntityTypeBuilder<OrdemServicoPagamento> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.NumeroParcela).IsRequired();
        builder.Property(c => c.Vencimento).IsRequired();
        builder.Property(c => c.ValorTotal).IsRequired().HasColumnType("decimal(18,2)");

        builder.HasOne(c => c.OrdemServicoTipoPagamento)
            .WithMany()
            .HasForeignKey(c => c.OrdemServicoTipoPagamentoId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}