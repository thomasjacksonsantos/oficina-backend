
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Domain.Aggregates.VeiculoAggregates;

namespace CarePath.Infrastructure.DataAccess.Mappings;

public sealed class EfVeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.HasKey(c => c.Id);

        builder.ComplexProperty(c => c.Criado, criado =>
        {
            criado.Property(c => c.Valor).HasColumnName(nameof(Veiculo.Criado));
        });

        builder.ComplexProperty(c => c.Atualizado, atualizado =>
        {
            atualizado.Property(c => c.Valor).HasColumnName(nameof(Veiculo.Atualizado));
        });
    }
}