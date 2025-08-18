using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.ContaAggregates;

public record TipoPagamento : DadoDominio
{
    private TipoPagamento() : base() { }
        
    private TipoPagamento(Guid id, string key) : base(id, key) { }

    public static readonly TipoPagamento Credito = ("5635808c-642c-4163-a14c-73b3938d7188", "Credito");
    public static readonly TipoPagamento Debito = ("39839079-5dd7-4009-86ba-980001b45627", "Debito");
    
    public static implicit operator TipoPagamento((string Id, string Key) data) =>
        new(new Guid(data.Id), data.Key);
}
