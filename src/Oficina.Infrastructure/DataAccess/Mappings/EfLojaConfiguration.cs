

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
        builder.Property(c => c.Nome).HasMaxLength(600);
        
        builder.ComplexProperty(c => c.Documento, documento =>
        {
            documento.Property(c => c.Numero).HasColumnName(nameof(Documento.Numero)).HasMaxLength(16);
            documento.ComplexProperty(c => c.TipoDocumento, tipoDocumento =>
            {
                tipoDocumento.Property(c => c.Id).HasColumnName(nameof(TipoDocumento.Id));
                tipoDocumento.Property(c => c.Nome).HasColumnName(nameof(TipoDocumento.Nome)).HasMaxLength(600);
            });
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