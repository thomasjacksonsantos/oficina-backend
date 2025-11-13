using Oficina.Domain.Aggregates.OrdemServicoAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarePath.Infrastructure.DataAccess.Mappings;

public sealed class EfOrdemServicoConfiguration : IEntityTypeConfiguration<OrdemServico>
{
    public void Configure(EntityTypeBuilder<OrdemServico> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Observacao).HasMaxLength(2000);

        builder.ComplexProperty(c => c.Criado, criado =>
        {
            criado.Property(c => c.Valor).HasColumnName(nameof(OrdemServico.Criado));
        });

        builder.ComplexProperty(c => c.Atualizado, atualizado =>
        {
            atualizado.Property(c => c.Valor).HasColumnName(nameof(OrdemServico.Atualizado));
        });

        builder.HasOne(c => c.OrdemServicoStatus)
            .WithOne()
            .HasForeignKey<OrdemServico>(c => c.OrdemServicoStatusId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}