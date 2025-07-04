



using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.Infrastructure.DomainImplementation.Aggregates.ClienteAggregates;

public sealed class EfClienteRepository(EfUnitOfWork<ApplicationDbContext> unitOfWork)
    : EfRepository<ApplicationDbContext, Cliente>(unitOfWork), IClienteRepository
{
}