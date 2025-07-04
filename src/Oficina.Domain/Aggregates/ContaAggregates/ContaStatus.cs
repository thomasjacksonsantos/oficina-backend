using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.ContaAggregates;

public record ContaStatus : DadoDominio
{
    private ContaStatus() : base() { }
        
    private ContaStatus(Guid id, string key) : base(id, key) { }

    public static readonly ContaStatus Ativo = ("019606a2-1b00-704e-a85d-f33f15a4e992", "Ativo");
    public static readonly ContaStatus Inativo = ("019606a2-5550-7c9d-9939-2eef3e1c5fda", "Inativo");
    
    public static implicit operator ContaStatus((string Id, string Key) data) =>
        new(new Guid(data.Id), data.Key);
}
