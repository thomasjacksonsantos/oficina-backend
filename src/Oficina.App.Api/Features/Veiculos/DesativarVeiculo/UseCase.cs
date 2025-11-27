


using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.Aggregates.VeiculoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Veiculos.DesativarVeiculo;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<DesativarVeiculoRequest, DesativarVeiculoResponse>
{
    public async Task<Result<DesativarVeiculoResponse>> Execute(
        DesativarVeiculoRequest input,
        CancellationToken ct = default
    )
    {
        var veiculo = await fluentQuery.For<Veiculo>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .FindFirstAsync(ct);

        if (veiculo == null)
            return Result.Fail(Erro.Validacao(nameof(input.Id), nameof(input.Id), "Veículo não encontrado."));

        veiculo.Desativar();

        await unitOfWork.SaveChangesAsync(ct);

        return new DesativarVeiculoResponse(
            "Veículo desativado com sucesso."
        );
    }
}
