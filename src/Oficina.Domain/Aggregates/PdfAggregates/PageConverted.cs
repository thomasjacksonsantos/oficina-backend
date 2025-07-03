

namespace Oficina.Domain.Aggregates.PdfAggregates;

public sealed class PageConverted
{

#pragma warning disable CS8618
    private PageConverted() { }
#pragma warning restore CS8618

    private PageConverted(
        int page,
        string textOriginal,
        byte[] imageData
    )
    {
        if (page <= 0)
            throw new ArgumentOutOfRangeException(nameof(page));

        if (string.IsNullOrWhiteSpace(textOriginal))
            throw new ArgumentNullException(nameof(textOriginal));

        if (imageData is null || imageData.Length == 0)
            throw new ArgumentOutOfRangeException(nameof(imageData));

        Page = page;
        TextOriginal = textOriginal;
        ImageData = imageData;
        CreatedAt = DateTime.Now;
    }

    public int Id { get; private set; }
    public int PdfRootId { get; private set; }
    public int Page { get; private set; }
    public byte[] ImageData { get; private set; }
    public string TextOriginal { get; private set; }
    public ICollection<PageLang>? Langs { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public void AddLang(PageLang lang)
    {
        Langs ??= new List<PageLang>();
        Langs.Add(lang);
    }

    public static PageConverted Create(
        int page,
        string textOriginal,
        byte[] imageData
    ) => new(page, textOriginal, imageData);
}
