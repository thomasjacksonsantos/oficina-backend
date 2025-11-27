



using Oficina.Domain.Aggregates.VeiculoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Veiculos.AtivarVeiculo;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtivarVeiculoRequest, AtivarVeiculoResponse>
{
    public async Task<Result<AtivarVeiculoResponse>> Execute(
        AtivarVeiculoRequest input,
        CancellationToken ct = default
    )
    {
        var veiculo = await fluentQuery.For<Veiculo>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithTracking()
            .FindFirstAsync(ct);

        if (veiculo == null)
            return Result.Fail(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(input.Id)}", "Veículo não encontrado."));

        veiculo.Ativar();

        await unitOfWork.SaveChangesAsync(ct);

        return new AtivarVeiculoResponse(
            "Veículo ativado com sucesso."
        );
    }
}
