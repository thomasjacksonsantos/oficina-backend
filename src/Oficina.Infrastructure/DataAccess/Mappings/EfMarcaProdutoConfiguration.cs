

using Oficina.Domain.Aggregates.MarcaProdutoAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Oficina.Infrastructure.DataAccess.Mappings;

public class EfMarcaProdutoConfiguration : IEntityTypeConfiguration<MarcaProduto>
{
    public void Configure(EntityTypeBuilder<MarcaProduto> builder)
    {
        builder.Property(c => c.Descricao).HasMaxLength(1000);
                
        builder.ComplexProperty(c => c.Criado, criado =>
        {
            criado.Property(c => c.Valor).HasColumnName(nameof(MarcaProduto.Criado));
        });
        builder.ComplexProperty(c => c.Atualizado, atualizado =>
        {
            atualizado.Property(c => c.Valor).HasColumnName(nameof(MarcaProduto.Atualizado));
        });

        builder.HasOne(c => c.MarcaProdutoStatus)
            .WithMany()
            .HasForeignKey(c => c.MarcaProdutoStatusId)
            .OnDelete(DeleteBehavior.NoAction); 
    }
}