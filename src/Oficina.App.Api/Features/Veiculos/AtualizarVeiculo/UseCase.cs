


using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.Aggregates.VeiculoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Veiculos.AtualizarVeiculo;

public sealed class UseCase(
    IRepository<Veiculo> veiculoRepository,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtualizarVeiculoRequest, AtualizarVeiculoResponse>
{
    public async Task<Result<AtualizarVeiculoResponse>> Execute(
        AtualizarVeiculoRequest input,
        CancellationToken ct = default
    )
    {
        var veiculo = await veiculoRepository.FindFirstByPredicate(
            predicate: c => c.Id == input.Id,
            ct
        ) ?? null!;

        if (veiculo == null)
            return Result.Fail(Erro.Validacao(nameof(input.Id), nameof(input.Id), "Veículo não encontrado."));

        var clienteResult = veiculo.Atualizar(
            input.Placa,
            input.Modelo,
            input.Montadora,
            input.Hodrometro,
            input.Cor,
            input.Ano,
            input.NumeroChassi,
            input.NumeroSerie,
            input.Motorizacao,
            input.Chassi
        );

        if (clienteResult.IsFailed)
            return Result.Fail(clienteResult.Errors!);

        await unitOfWork.SaveChangesAsync(ct);

        return new AtualizarVeiculoResponse();
    }
}
