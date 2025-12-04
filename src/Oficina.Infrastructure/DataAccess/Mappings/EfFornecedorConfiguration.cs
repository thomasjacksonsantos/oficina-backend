

using Oficina.Domain.Aggregates.FornecedorAggregates;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Oficina.Infrastructure.DataAccess.Mappings;

public class EfFornecedorConfiguration : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.Property(c => c.NomeFantasia).HasMaxLength(600);
        builder.Property(c => c.Site).HasMaxLength(1000);
        builder.Property(c => c.InscricaoEstadual).HasMaxLength(600);
        builder.Property(c => c.InscricaoMunicipal).HasMaxLength(600);

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

        builder.HasOne(c => c.FornecedorStatus)
            .WithMany()
            .HasForeignKey(c => c.FornecedorStatusId)
            .OnDelete(DeleteBehavior.NoAction); 

        builder.HasOne(c => c.TipoConsumidor)
            .WithMany()
            .HasForeignKey(c => c.TipoConsumidorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}