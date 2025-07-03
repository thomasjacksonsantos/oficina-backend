

using Oficina.Domain.Enums;

namespace Oficina.Domain.Aggregates.PdfAggregates;

public sealed class PageLang
{

#pragma warning disable CS8618
    private PageLang() { }
#pragma warning restore CS8618

    public int Id { get; private set; }
    public string Text { get; private set; }
    public LangEnum Lang { get; private set; }
    public DateTime CreatedAt { get; private set; }

}
