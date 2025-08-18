


using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.OrdemServicoAggregates;

public class ProdutoServicoItem
{
    public int Id { get; private set; }
    public int ProdutoId { get; private set; }
    public int Quantidade { get; private set; }
    public decimal ValorUnitario { get; private set; }
    public decimal Desconto { get; private set; }
    public decimal ValorLiquido { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }


#pragma warning disable CS8618 // Para uso do EF/serialização
    private ProdutoServicoItem() { }
#pragma warning restore CS8618

    private ProdutoServicoItem(
        int produtoId,
        int quantidade,
        decimal valorUnitario,
        decimal desconto,
        decimal valorLiquido)
    {
        ProdutoId = produtoId;
        Quantidade = quantidade;
        ValorUnitario = valorUnitario;
        Desconto = desconto;
        ValorLiquido = valorLiquido;
        Criado = DataHora.Criar().Value!;
        Atualizado = DataHora.Criar().Value!;
    }

    public static Result<ProdutoServicoItem> Criar(
        int produtoId,
        int quantidade,
        decimal valorUnitario,
        decimal desconto,
        decimal valorLiquido)
    {
        // Aqui poderia haver validações de negócio
        if (quantidade <= 0)
            return Result.Fail("A quantidade deve ser maior que zero.");

        if (valorUnitario < 0)
            return Result.Fail("O valor unitário não pode ser negativo.");

        var item = new ProdutoServicoItem(
            produtoId,
            quantidade,
            valorUnitario,
            desconto,
            valorLiquido
        );

        return Result.Success(item);
    }
}