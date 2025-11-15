using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Domain.Aggregates.UsuarioAggregates;

namespace Oficina.Infrastructure.DataAccess.Mappings;

public class EfUsuarioContextoConfiguration : IEntityTypeConfiguration<UsuarioContexto>
{
    public void Configure(EntityTypeBuilder<UsuarioContexto> builder)
    {        
        builder.HasOne(c => c.Conta)
            .WithOne()
            .HasForeignKey<UsuarioContexto>(c => c.ContaId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.Loja)
            .WithOne()
            .HasForeignKey<UsuarioContexto>(c => c.LojaId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}