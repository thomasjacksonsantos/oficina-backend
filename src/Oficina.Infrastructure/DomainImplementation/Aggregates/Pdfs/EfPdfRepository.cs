


using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.Aggregates.PdfAggregates;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.Infrastructure.DomainImplementation.Aggregates.PdfAggregates;

public sealed class EfPdfRepository(EfUnitOfWork<ApplicationDbContext> unitOfWork)
    : EfRepository<ApplicationDbContext, PdfRoot>(unitOfWork), IPdfRepository
{
    public ValueTask<IEnumerable<PdfRoot>> FindAllAsync(int page = 0, int? total = 20)
    {
        throw new NotImplementedException();
    }

    public ValueTask<PdfRoot> FindAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<ICollection<PdfRoot>> FindByIds(IEnumerable<int> ids)
    {
        throw new NotImplementedException();
    }
}