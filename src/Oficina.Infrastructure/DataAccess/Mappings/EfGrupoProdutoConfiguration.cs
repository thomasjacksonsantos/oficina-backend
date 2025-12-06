

using Oficina.Domain.Aggregates.GrupoProdutoAggregates;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Oficina.Infrastructure.DataAccess.Mappings;

public class EfGrupoProdutoConfiguration : IEntityTypeConfiguration<GrupoProduto>
{
    public void Configure(EntityTypeBuilder<GrupoProduto> builder)
    {
        builder.Property(c => c.ANP).HasMaxLength(600);
        builder.Property(c => c.NCM).HasMaxLength(600);
        builder.Property(c => c.Descricao).HasMaxLength(1000);
        builder.Property(c => c.Area).HasMaxLength(600);
                
        builder.ComplexProperty(c => c.Criado, criado =>
        {
            criado.Property(c => c.Valor).HasColumnName(nameof(Usuario.Criado));
        });
        builder.ComplexProperty(c => c.Atualizado, atualizado =>
        {
            atualizado.Property(c => c.Valor).HasColumnName(nameof(Usuario.Atualizado));
        });

        builder.HasOne(c => c.GrupoProdutoStatus)
            .WithMany()
            .HasForeignKey(c => c.GrupoProdutoStatusId)
            .OnDelete(DeleteBehavior.NoAction); 
    }
}