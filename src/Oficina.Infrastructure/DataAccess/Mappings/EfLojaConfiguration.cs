

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Oficina.Domain.Aggregates.LojaAggregates;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.ValueObjects;

namespace Oficina.Infrastructure.DataAccess.Mappings;

public class EfLojaConfiguration : IEntityTypeConfiguration<Loja>
{
    public void Configure(EntityTypeBuilder<Loja> builder)
    {
        builder.Property(c => c.NomeFantasia).HasMaxLength(600);
        builder.Property(c => c.RazaoSocial).HasMaxLength(600);
        builder.Property(c => c.InscricaoEstadual).HasMaxLength(600);
        builder.Property(c => c.Site).HasMaxLength(600);
        builder.Property(c => c.LogoTipo).HasColumnType("text");
        
        builder.ComplexProperty(c => c.Documento, documento =>
        {
            documento.Property(c => c.Numero).HasColumnName(nameof(Documento.Numero)).HasMaxLength(16);
        });        
        builder
        .Property(e => e.Contatos)
        .HasConversion(
            convertToProviderExpression: c =>
                JsonConvert.SerializeObject(
                    c,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }
                ),
            convertFromProviderExpression: c =>
                JsonConvert.DeserializeObject<ICollection<Contato>>(
                    c,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }
                )!
        );
        builder
        .Property(e => e.Endereco)
        .HasConversion(
            convertToProviderExpression: c =>
                JsonConvert.SerializeObject(
                    c,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }
                ),
            convertFromProviderExpression: c =>
                JsonConvert.DeserializeObject<Endereco>(
                    c,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }
                )!
        );
        builder.ComplexProperty(c => c.Criado, criado =>
        {
            criado.Property(c => c.Valor).HasColumnName(nameof(Usuario.Criado));
        });
        builder.ComplexProperty(c => c.Atualizado, atualizado =>
        {
            atualizado.Property(c => c.Valor).HasColumnName(nameof(Usuario.Atualizado));
        });
    }
}