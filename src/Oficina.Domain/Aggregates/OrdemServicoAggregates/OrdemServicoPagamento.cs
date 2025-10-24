using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.SeedWork;

namespace Oficina.Domain.Aggregates.OrdemServicoAggregates;

public class OrdemServicoPagamento
{
    public int Id { get; private set; }
    public int NumeroParcela { get; private set; }
    public DateTime Vencimento { get; private set; }
    public Guid OrdemServicoTipoPagamentoId { get; private set; }
    public decimal ValorTotal { get; private set; }


#pragma warning disable CS8618 // Propriedade não inicializada
    private OrdemServicoPagamento() { }
#pragma warning restore CS8618

    public OrdemServicoPagamento(
        int numeroParcela,
        DateTime vencimento,
        Guid ordemServicoTipoPagamentoId,
        decimal valorTotal
    )
    {
        NumeroParcela = numeroParcela;
        Vencimento = vencimento;
        OrdemServicoTipoPagamentoId = ordemServicoTipoPagamentoId;
        ValorTotal = valorTotal;
    }

    public static Result<OrdemServicoPagamento> Criar(
        int numeroParcela,
        DateTime vencimento,
        string tipoPagamento,
        decimal valorTotal
    )
    {
        var result = new Result();

        if (numeroParcela <= 0)
            result.WithError(Erro.ValorInvalido("OrdemServicoPagamento.numeroParcela"));

        if (vencimento.Date < DateTime.UtcNow.Date)
            result.WithError(Erro.ValorInvalido("OrdemServicoPagamento.vencimento"));

        var ordemServicoTipoPagamento = OrdemServicoTipoPagamento.Get(tipoPagamento);
        if (ordemServicoTipoPagamento == null)
            result.WithError(Erro.ValorInvalido("OrdemServicoPagamento.tipoPagamento"));

        if (valorTotal <= 0)
            result.WithError(Erro.ValorInvalido("OrdemServicoPagamento.valorTotal"));

        if (result.IsFailed)
            return Result.Fail(result.Errors!);

        var pagamento = new OrdemServicoPagamento(numeroParcela, vencimento, ordemServicoTipoPagamento!.Id, valorTotal);
        return Result.Success(pagamento);
    }
}