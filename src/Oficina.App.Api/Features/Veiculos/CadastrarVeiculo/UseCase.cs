


using Oficina.Domain.Aggregates.VeiculoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Veiculos.CadastrarVeiculo;

public sealed class UseCase(
    IRepository<Veiculo> veiculoRepository,
    IUnitOfWork unitOfWork
)
    : IUseCase<CadastrarVeiculoRequest, CadastrarVeiculoResponse>
{
    public async Task<Result<CadastrarVeiculoResponse>> Execute(
        CadastrarVeiculoRequest input,
        CancellationToken ct = default
    )
    {
        var veiculoResult = Veiculo.Criar(
            input.Placa,
            input.Modelo,
            input.Montadora,
            input.Hodrometro,
            input.Cor,
            input.Ano,
            input.NumeroSerie,
            input.Motorizacao,
            input.Chassi
        );

        if (veiculoResult.IsFailed)
            return Result.Fail(veiculoResult.Errors!);

        await veiculoRepository.AddAsync(veiculoResult.Value!);
        await unitOfWork.SaveChangesAsync(ct);

        return new CadastrarVeiculoResponse(veiculoResult.Value!.Id);
    }
}
