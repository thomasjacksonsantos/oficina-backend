
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Domain.Aggregates.ProdutoAggregates;

namespace CarePath.Infrastructure.DataAccess.Mappings;

public sealed class EfProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(c => c.Id);

        builder.ComplexProperty(c => c.Criado, criado =>
        {
            criado.Property(c => c.Valor).HasColumnName(nameof(Produto.Criado));
        });

        builder.ComplexProperty(c => c.Atualizado, atualizado =>
        {
            atualizado.Property(c => c.Valor).HasColumnName(nameof(Produto.Atualizado));
        });
    }
}