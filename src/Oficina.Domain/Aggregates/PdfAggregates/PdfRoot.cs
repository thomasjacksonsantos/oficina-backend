

namespace Oficina.Domain.Aggregates.PdfAggregates;

public sealed class PdfRoot
{

#pragma warning disable CS8618
    private PdfRoot() { }
#pragma warning restore CS8618

    private PdfRoot(
        string filename,
        int totalPages
    )
    {
        if (string.IsNullOrWhiteSpace(filename))
            throw new ArgumentNullException(nameof(filename));

        if (totalPages <= 0)
            throw new ArgumentOutOfRangeException(nameof(totalPages));

        FileName = filename;
        TotalPages = totalPages;
        CreatedAt = DateTime.Now;
    }

    public int Id { get; private set; }
    public string FileName { get; private set; }
    public int TotalPages { get; private set; }
    public ICollection<PageConverted>? Pages { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public void AddPageConverted(PageConverted page)
    {
        Pages ??= new List<PageConverted>();
        Pages.Add(page);
    }

    public static PdfRoot Create(
        string filename,
        int totalPages
    ) => new(filename, totalPages);
}
