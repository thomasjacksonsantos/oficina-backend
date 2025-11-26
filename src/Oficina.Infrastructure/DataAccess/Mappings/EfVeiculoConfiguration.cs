
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Domain.Aggregates.VeiculoAggregates;

namespace CarePath.Infrastructure.DataAccess.Mappings;

public sealed class EfVeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Placa).HasMaxLength(10);
        builder.Property(c => c.Modelo).HasMaxLength(100);
        builder.Property(c => c.Montadora).HasMaxLength(100);
        builder.Property(c => c.Cor).HasMaxLength(50);
        builder.Property(c => c.NumeroSerie).HasMaxLength(50);
        builder.Property(c => c.Motorizacao).HasMaxLength(50);
        builder.Property(c => c.Chassi).HasMaxLength(50);

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