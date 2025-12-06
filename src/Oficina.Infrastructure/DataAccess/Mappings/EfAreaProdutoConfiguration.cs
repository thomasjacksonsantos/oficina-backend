

using Oficina.Domain.Aggregates.AreaProdutoAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Oficina.Infrastructure.DataAccess.Mappings;

public class EfAreaProdutoConfiguration : IEntityTypeConfiguration<AreaProduto>
{
    public void Configure(EntityTypeBuilder<AreaProduto> builder)
    {
        builder.Property(c => c.Garantia);
        builder.Property(c => c.Descricao).HasMaxLength(1000);
                
        builder.ComplexProperty(c => c.Criado, criado =>
        {
            criado.Property(c => c.Valor).HasColumnName(nameof(AreaProduto.Criado));
        });
        builder.ComplexProperty(c => c.Atualizado, atualizado =>
        {
            atualizado.Property(c => c.Valor).HasColumnName(nameof(AreaProduto.Atualizado));
        });

        builder.HasOne(c => c.AreaProdutoStatus)
            .WithMany()
            .HasForeignKey(c => c.AreaProdutoStatusId)
            .OnDelete(DeleteBehavior.NoAction); 
    }
}