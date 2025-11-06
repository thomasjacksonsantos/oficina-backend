using Oficina.Domain.Aggregates.LojaAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.ProdutoAggregates;

public class PrecoLoja
{
    public int Id { get; private set; }
    public decimal Valor { get; private set; }
    public int LojaId { get; private set; }
    public Loja? Loja { get; private set; }
    public Guid ProdutoId { get; private set; }
    public Produto? Produto { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

#pragma warning disable CS8618 // Para uso do EF/serialização
    private PrecoLoja() { }
#pragma warning restore CS8618

    private PrecoLoja(
        int lojaId,
        Guid produtoId,
        decimal valor
    )
    {
        LojaId = lojaId;
        ProdutoId = produtoId;
        Valor = valor;
        Criado = DataHora.Criar().Value!;
        Atualizado = DataHora.Criar().Value!;
    }

    public static Result<PrecoLoja> Criar(
        int lojaId,
        Guid produtoId,
        decimal valor
    )
    {
        var result = new Result<PrecoLoja>();

        if (produtoId == Guid.Empty)
            result.WithError(Erro.ValorInvalido("PrecoLoja.ProdutoId"));

        if (valor <= 0)
            result.WithError(Erro.ValorInvalido("PrecoLoja.Valor"));

        if (result.IsFailed)
            return result;

        var precoLoja = new PrecoLoja(lojaId, produtoId, valor);

        return Result.Success(precoLoja);
    }
}