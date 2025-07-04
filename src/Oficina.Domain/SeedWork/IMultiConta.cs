using Oficina.Domain.Aggregates.ContaAggregates;

namespace Oficina.Domain.SeedWork;

public interface IMultiConta
{
    int ContaId { get; }
    
    Conta Conta { get; }
} 