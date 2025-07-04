
using Oficina.Infrastructure.DataAccess;
using Oficina.Domain.Aggregates.UsuarioAggregates;

namespace CarePath.Infrastructure.DomainImplementation.Aggregates.UsuarioAggregates;

public sealed class EfUsuarioRepository(
    EfUnitOfWork<ApplicationDbContext> unitOfWork
)
    : EfRepository<ApplicationDbContext, Usuario>(unitOfWork), IUsuarioRepository
{
}