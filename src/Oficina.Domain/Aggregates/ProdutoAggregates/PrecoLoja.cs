using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.ProdutoAggregates;

public class PrecoLoja
{
    public int Id { get; private set; }
    public decimal Valor { get; private set; }
    public int ProdutoId { get; private set; }
    public Produto Produto { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

#pragma warning disable CS8618 // Para uso do EF/serialização
    private PrecoLoja() { }
#pragma warning restore CS8618

    private PrecoLoja(
        Produto produto,
        decimal valor
    )
    {
        ProdutoId = produto.Id;
        Produto = produto;
        Valor = valor;
        Criado = DataHora.Criar().Value!;
        Atualizado = DataHora.Criar().Value!;
    }

    public static Result<PrecoLoja> Criar(
        Produto produto,
        decimal valor
    )
    {
        if (produto is null)
            return Result.Fail("O Produto não pode ser nulo.");

        if (valor <= 0)
            return Result.Fail("O Valor não pode ser ter o valor zero.");

        var precoLoja = new PrecoLoja(produto, valor);

        return Result.Success(precoLoja);
    }
}