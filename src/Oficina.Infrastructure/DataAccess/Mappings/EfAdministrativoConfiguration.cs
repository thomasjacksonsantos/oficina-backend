
// using Oficina.Domain.Aggregates.UsuarioAggregates.Perfis;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Oficina.Infrastructure.DataAccess.Mappings;

// public class EfAdministrativoConfiguration : IEntityTypeConfiguration<Administrativo>
// {
//     public void Configure(EntityTypeBuilder<Administrativo> builder)
//     {
//         builder.ComplexProperty(c => c.PisPasep, pisPasep =>
//         {
//             pisPasep.Property(c => c.Valor).HasColumnName(nameof(Administrativo.PisPasep)).HasMaxLength(50);
//         });

//         builder.ComplexProperty(c => c.Observacao, observacao =>
//         {
//             observacao.Property(c => c.Valor).HasColumnName(nameof(Administrativo.Observacao)).HasMaxLength(600);
//         });
//     }
// }