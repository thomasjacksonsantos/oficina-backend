using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.SeedWork;

namespace Oficina.Aggregates.ContaAggregates;

public class PagamentoConta
{
    public int Id { get; private set; }
    public int ContaId { get; private set; }
    public Conta? Conta { get; private set; }
    public DateTime DataPagamento { get; private set; } // Payment Date
    public decimal ValorPago { get; private set; } // Paid Value
    public bool Pago { get; private set; } // Paid Status
    public string Referencia { get; private set; } // Reference (ex: mês/ano)

#pragma warning disable CS8618
    private PagamentoConta() { }
#pragma warning restore CS8618

    private PagamentoConta(
        int contaId,
        decimal valorPago,
        DateTime dataPagamento,
        string referencia
    )
    {
        ContaId = contaId;
        ValorPago = valorPago;
        DataPagamento = dataPagamento;
        Referencia = referencia;
        Pago = true;
    }

    public static Result<PagamentoConta> Criar(
        int contaId,
        decimal valorPago,
        DateTime dataPagamento,
        string referencia
    )
    {
        var result = new Result<PagamentoConta>();

        if (contaId <= 0)
            result.WithError(Erro.ValorInvalido(
                "PagamentoConta.ContaId",
                "O ID da conta é inválido."
            ));

        if (valorPago <= 0)
            result.WithError(Erro.ValorInvalido(
                "PagamentoConta.ValorPago",
                "O valor pago deve ser maior que zero."
            ));

        if (dataPagamento <= DateTime.MinValue || dataPagamento >= DateTime.MaxValue)
            result.WithError(Erro.ValorInvalido(
                "PagamentoConta.DataPagamento",
                "A data de pagamento é inválida."
            ));

        if (string.IsNullOrWhiteSpace(referencia))
            result.WithError(Erro.ValorInvalido(
                "PagamentoConta.Referencia",
                "A referência do pagamento é inválida."
            ));

        if (result.IsFailed)
            return result;

        return new PagamentoConta(
            contaId,
            valorPago,
            dataPagamento,
            referencia
        );
    }
}