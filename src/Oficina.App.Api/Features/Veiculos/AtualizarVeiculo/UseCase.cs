



using Oficina.Domain.Aggregates.VeiculoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Veiculos.AtualizarVeiculo;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtualizarVeiculoRequest, AtualizarVeiculoResponse>
{
    public async Task<Result<AtualizarVeiculoResponse>> Execute(
        AtualizarVeiculoRequest input,
        CancellationToken ct = default
    )
    {
        var veiculo = await fluentQuery.For<Veiculo>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithTracking()
            .FindFirstAsync(ct);

        if (veiculo == null)
            return Result.Fail(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(input.Id)}", "Veículo não encontrado."));

        veiculo.Atualizar(
            input.Modelo,
            input.Montadora,
            input.Hodrometro,
            input.Cor,
            input.Ano,
            input.NumeroSerie,
            input.Motorizacao,
            input.Chassi
        );

        await unitOfWork.SaveChangesAsync(ct);

        return new AtualizarVeiculoResponse(
            "Veículo ativado com sucesso."
        );
    }
}
