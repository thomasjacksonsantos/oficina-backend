

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.Enumerations;
using Oficina.Domain.ValueObjects;

namespace Oficina.Infrastructure.DataAccess.Mappings;

public class EfClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.Property(c => c.Nome).HasMaxLength(600);
        builder.Property(c => c.RazaoSocial).HasMaxLength(600);

        builder.ComplexProperty(c => c.Documento, documento =>
        {
            documento.Property(c => c.Numero).HasColumnName(nameof(Documento.Numero)).HasMaxLength(16);
        });

        builder.ComplexProperty(c => c.Email, email =>
        {
            email.Property(c => c.Valor).HasColumnName(nameof(Email.Valor)).HasMaxLength(800);
            email.Property(c => c.Valor).HasColumnName(nameof(Email.Principal));
        });
        builder.ComplexProperty(c => c.DataNascimento, dataNascimento =>
        {
            dataNascimento.Property(c => c.Valor).HasColumnName(nameof(DataNascimento.Valor));
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

        builder.HasOne(c => c.Sexo)
            .WithMany()
            .HasForeignKey(c => c.SexoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.TipoDocumento)
            .WithMany()
            .HasForeignKey(c => c.TipoDocumentoId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.ClienteStatus)
            .WithMany()
            .HasForeignKey(c => c.ClienteStatusId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}