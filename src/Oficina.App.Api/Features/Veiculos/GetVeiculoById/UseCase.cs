


using Oficina.Domain.Aggregates.VeiculoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Veiculos.GetVeiculoById;

public sealed class UseCase(
    IRepository<Veiculo> veiculoRepository
)
    : IUseCase<GetVeiculoByIdRequest, GetVeiculoByIdResponse>
{
    public async Task<Result<GetVeiculoByIdResponse>> Execute(
        GetVeiculoByIdRequest input,
        CancellationToken ct = default
    )
    {
        var veiculo = await veiculoRepository.FindFirstByPredicate(
            predicate: c => c.Id == input.Id,
            ct
        ) ?? null!;


        return new GetVeiculoByIdResponse(
            veiculo.Placa,
            veiculo.Modelo,
            veiculo.Montadora,
            veiculo.Hodrometro,
            veiculo.Cor,
            veiculo.Ano,
            veiculo.NumeroSerie,
            veiculo.Motorizacao,
            veiculo.Chassi,
            veiculo.VeiculoStatus.Key
        );
    }
}