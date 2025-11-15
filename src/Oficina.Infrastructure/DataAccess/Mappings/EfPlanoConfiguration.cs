using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Domain.Aggregates.PlanoAggregates;

namespace Oficina.Infrastructure.DataAccess.Mappings;

public class EfPlanoConfiguration : IEntityTypeConfiguration<Plano>
{
    public void Configure(EntityTypeBuilder<Plano> builder)
    {
        builder.Property(c => c.Nome).HasMaxLength(600);

        builder.HasOne(c => c.PlanoStatus)
            .WithMany()
            .HasForeignKey(c => c.PlanoStatusId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}