
using Oficina.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Oficina.Infrastructure.DataAccess.Mappings;

public sealed class EfDadoDominioConfiguration : IEntityTypeConfiguration<DadoDominio>
{
    public void Configure(EntityTypeBuilder<DadoDominio> builder)
    {
        builder
            .Property( x => x.Id )
            .ValueGeneratedNever();
        
        builder
            .Property(x => x.Nome)
            .HasMaxLength(100);
        
        builder
            .Property(x => x.Key)
            .HasMaxLength(50);
        
        builder
            .HasDiscriminator(x => x.Dominio);
        
        builder
            .HasIndex(x => new { x.Key, x.Dominio })
            .IsUnique();
    }
}