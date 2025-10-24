using System.Dynamic;
using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.OrdemServicoAggregates;

public record OrdemServicoTipoPagamento : DadoDominio
{
    private OrdemServicoTipoPagamento() : base() { }

    private OrdemServicoTipoPagamento(Guid id, string key) : base(id, key) { }

    public static OrdemServicoTipoPagamento Get(string key) =>
        key.ToLower() switch
        {
            "credito" => Credito,
            "debito" => Debito,
            _ => throw new ArgumentOutOfRangeException($"Tipo de pagamento inválido: {key}")
        };

    public static OrdemServicoTipoPagamento Get(Guid id) =>
        id == Credito.Id ? Credito :
        id == Debito.Id ? Debito :
        throw new ArgumentOutOfRangeException($"Tipo de pagamento inválido: {id}");

    public static readonly OrdemServicoTipoPagamento Credito = ("5635808c-642c-4163-a14c-73b3938d7188", "Credito");
    public static readonly OrdemServicoTipoPagamento Debito = ("39839079-5dd7-4009-86ba-980001b45627", "Debito");

    public static implicit operator OrdemServicoTipoPagamento((string Id, string Key) data) =>
        new(new Guid(data.Id), data.Key);
}
