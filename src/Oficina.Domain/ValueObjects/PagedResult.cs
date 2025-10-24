namespace Oficina.Domain.ValueObjects;

public record PagedResult<T>
{
    public int PaginaAtual { get; init; }
    public int Limite { get; init; }
    public int TotalRegistros { get; init; }
    public int TotalPaginas { get => Convert.ToInt32(Math.Ceiling(TotalRegistros / (double)Limite)); }
    public IEnumerable<T> Dados { get; init; } = new List<T>();

    public PagedResult<TTarget> MapTo<TTarget>(Func<T, TTarget> mapper)
    {
        return new PagedResult<TTarget> 
        {
            PaginaAtual = this.PaginaAtual,
            Limite = this.Limite,
            TotalRegistros = this.TotalRegistros,
            Dados = this.Dados.Select(mapper)
        };
    }
}