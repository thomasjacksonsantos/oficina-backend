using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Infrastructure.IO;

public record CepParams(string Cep);

public interface ICep
{
    // public ValueTask<Result<ConsultaCep>> Consultar(CepParams cepParams, CancellationToken ct = default);
}