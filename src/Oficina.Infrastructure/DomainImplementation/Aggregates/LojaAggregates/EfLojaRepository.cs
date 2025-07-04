



using Oficina.Domain.Aggregates.LojaAggregates;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.Infrastructure.DomainImplementation.Aggregates.LojaAggregates;

public sealed class EfLojaRepository(EfUnitOfWork<ApplicationDbContext> unitOfWork)
    : EfRepository<ApplicationDbContext, Loja>(unitOfWork), ILojaRepository
{
}