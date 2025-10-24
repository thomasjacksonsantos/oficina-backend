
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Domain.Aggregates.OrdemServicoAggregates;

namespace CarePath.Infrastructure.DataAccess.Mappings;

public sealed class EfOrdemServicoItemConfiguration : IEntityTypeConfiguration<OrdemServicoItem>
{
    public void Configure(EntityTypeBuilder<OrdemServicoItem> builder)
    {
        builder.HasKey(c => c.Id);

        builder.ComplexProperty(c => c.Criado, criado =>
        {
            criado.Property(c => c.Valor).HasColumnName(nameof(OrdemServicoItem.Criado));
        });

        builder.ComplexProperty(c => c.Atualizado, atualizado =>
        {
            atualizado.Property(c => c.Valor).HasColumnName(nameof(OrdemServicoItem.Atualizado));
        });
    }
}