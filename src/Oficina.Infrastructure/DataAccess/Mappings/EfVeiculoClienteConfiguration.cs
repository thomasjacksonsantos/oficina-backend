
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Domain.Aggregates.VeiculoAggregates;

namespace CarePath.Infrastructure.DataAccess.Mappings;

public sealed class EfVeiculoClienteConfiguration : IEntityTypeConfiguration<VeiculoCliente>
{
    public void Configure(EntityTypeBuilder<VeiculoCliente> builder)
    {
        builder.HasKey(c => c.Id);

        builder.ComplexProperty(c => c.Criado, criado =>
        {
            criado.Property(c => c.Valor).HasColumnName(nameof(VeiculoCliente.Criado));
        });

        builder.ComplexProperty(c => c.Atualizado, atualizado =>
        {
            atualizado.Property(c => c.Valor).HasColumnName(nameof(VeiculoCliente.Atualizado));
        });
    }
}