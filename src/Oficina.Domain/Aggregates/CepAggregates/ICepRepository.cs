
using Oficina.Domain.SeedWork;

namespace Oficina.Domain.Aggregates.CepAggregates;
public record CepParams(string Cep);

public interface ICepRepository
{
    public ValueTask<Result<ConsultaCep>> Consultar(CepParams cepParams, CancellationToken ct = default);
}