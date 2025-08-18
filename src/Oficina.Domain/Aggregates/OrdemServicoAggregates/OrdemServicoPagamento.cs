using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.SeedWork;

namespace Oficina.Domain.Aggregates.OrdemServicoAggregates;

public class OrdemServicoPagamento
{
    public int Id { get; private set; }
    public int NumeroParcela { get; private set; }
    public DateTime Vencimento { get; private set; }
    public TipoPagamento TipoPagamento { get; private set; }
    public decimal ValorTotal { get; private set; }


#pragma warning disable CS8618 // Propriedade não inicializada
    private OrdemServicoPagamento() { }
#pragma warning restore CS8618

    public OrdemServicoPagamento(
        int numeroParcela,
        DateTime vencimento,
        TipoPagamento tipoPagamento,
        decimal valorTotal
    )
    {
        NumeroParcela = numeroParcela;
        Vencimento = vencimento;
        TipoPagamento = tipoPagamento;
        ValorTotal = valorTotal;
    }

    public static Result<OrdemServicoPagamento> Criar(
        int numeroParcela,
        DateTime vencimento,
        TipoPagamento tipoPagamento,
        decimal valorTotal
    )
    {
        var erros = new List<string>();

        if (numeroParcela <= 0)
            erros.Add("Número da parcela deve ser maior que zero.");

        if (vencimento.Date < DateTime.UtcNow.Date)
            erros.Add("Vencimento não pode ser em data passada.");

        if (!Enum.IsDefined(typeof(TipoPagamento), tipoPagamento))
            erros.Add("Tipo de pagamento inválido.");

        if (valorTotal <= 0)
            erros.Add("Valor total deve ser maior que zero.");

        if (erros.Any())
            return Result.Fail(string.Join(" ", erros));

        var pagamento = new OrdemServicoPagamento(numeroParcela, vencimento, tipoPagamento, valorTotal);
        return Result.Success(pagamento);
    }
}