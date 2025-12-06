

using Oficina.Domain.Aggregates.UnidadeProdutoAggregates;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Oficina.Infrastructure.DataAccess.Mappings;

public class EfUnidadeProdutoConfiguration : IEntityTypeConfiguration<UnidadeProduto>
{
    public void Configure(EntityTypeBuilder<UnidadeProduto> builder)
    {
        builder.Property(c => c.Descricao).HasMaxLength(1000);
                
        builder.ComplexProperty(c => c.Criado, criado =>
        {
            criado.Property(c => c.Valor).HasColumnName(nameof(UnidadeProduto.Criado));
        });
        builder.ComplexProperty(c => c.Atualizado, atualizado =>
        {
            atualizado.Property(c => c.Valor).HasColumnName(nameof(UnidadeProduto.Atualizado));
        });

        builder.HasOne(c => c.UnidadeProdutoStatus)
            .WithMany()
            .HasForeignKey(c => c.UnidadeProdutoStatusId)
            .OnDelete(DeleteBehavior.NoAction); 
    }
}