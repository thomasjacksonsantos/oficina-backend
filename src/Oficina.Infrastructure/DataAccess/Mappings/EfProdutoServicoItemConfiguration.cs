
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Domain.Aggregates.OrdemServicoAggregates;

namespace CarePath.Infrastructure.DataAccess.Mappings;

public sealed class EfProdutoServicoItemConfiguration : IEntityTypeConfiguration<ProdutoServicoItem>
{
    public void Configure(EntityTypeBuilder<ProdutoServicoItem> builder)
    {
        builder.HasKey(c => c.Id);

        builder.ComplexProperty(c => c.Criado, criado =>
        {
            criado.Property(c => c.Valor).HasColumnName(nameof(ProdutoServicoItem.Criado));
        });

        builder.ComplexProperty(c => c.Atualizado, atualizado =>
        {
            atualizado.Property(c => c.Valor).HasColumnName(nameof(ProdutoServicoItem.Atualizado));
        });
    }
}