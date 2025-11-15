using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Aggregates.ContaAggregates;

namespace Oficina.Infrastructure.DataAccess.Mappings;

public class EfPagamentoContaConfiguration : IEntityTypeConfiguration<PagamentoConta>
{
    public void Configure(EntityTypeBuilder<PagamentoConta> builder)
    {
        builder.Property(c => c.Referencia).HasMaxLength(50);
    }
}