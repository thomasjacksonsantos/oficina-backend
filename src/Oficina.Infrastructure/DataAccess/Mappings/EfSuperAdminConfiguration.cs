

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oficina.Domain.Aggregates.UsuarioAggregates;

namespace Oficina.Infrastructure.DataAccess.Mappings;

public class EfAdministrativoConfiguration : IEntityTypeConfiguration<SuperAdmin>
{
    public void Configure(EntityTypeBuilder<SuperAdmin> builder)
    {
    }
}