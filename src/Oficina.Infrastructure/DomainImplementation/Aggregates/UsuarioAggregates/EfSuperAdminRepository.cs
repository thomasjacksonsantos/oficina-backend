


using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Infrastructure.DataAccess;

namespace CarePath.Infrastructure.DomainImplementation.Aggregates.UsuarioAggregates;

public sealed class EfSuperAdminRepository(
    EfUnitOfWork<ApplicationDbContext> unitOfWork
)
    : EfRepository<ApplicationDbContext, SuperAdmin>(unitOfWork), ISuperAdminRepository
{
}