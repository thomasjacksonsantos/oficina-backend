
using Oficina.Domain.Aggregates.CepAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;

namespace Oficina.App.Api.Features.Cep;

public class UseCase(
    ICepRepository cep)
    : IUseCase<CepRequest, CepResponse>
{
    public async Task<Result<CepResponse>> Execute(
        CepRequest input,
        CancellationToken ct = default)
    {
        var consulta = await cep.Consultar(new CepParams(input.Cep), ct);

        if (consulta.IsSuccess)
            return new CepResponse(
                consulta.Value!.Pais,
                consulta.Value.Estado,
                consulta.Value.Cidade,
                consulta.Value.Logradouro,
                consulta.Value.Bairro,
                consulta.Value.Complemento,
                consulta.Value.Latitude,
                consulta.Value.Longitude
            );

        return Result<CepResponse>.Fail(consulta.Errors!);
    }
}