

using Oficina.Domain.Aggregates.PdfAggregates;

namespace Oficina.Domain.Aggregates.ContaAggregates;

public interface IPdfRepository
{
    ValueTask<PdfRoot> FindAsync(int Id);
    ValueTask<ICollection<PdfRoot>> FindByIds(IEnumerable<int> ids);
    ValueTask<IEnumerable<PdfRoot>> FindAllAsync(
        int page = 0,
        int? total = 20
    );
}