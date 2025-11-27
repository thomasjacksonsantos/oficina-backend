


using Oficina.Domain.Aggregates.VeiculoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Veiculos.CadastrarVeiculo;

public sealed class UseCase(
    IRepository<Veiculo> veiculoRepository,
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<CadastrarVeiculoRequest, CadastrarVeiculoResponse>
{
    public async Task<Result<CadastrarVeiculoResponse>> Execute(
        CadastrarVeiculoRequest input,
        CancellationToken ct = default
    )
    {
        var veiculoExists = await fluentQuery.For<Veiculo>()
            .WithPredicate(x => x.Placa == input.Placa)
            .CountAsync();

        if (veiculoExists > 0)
            return Result.Fail(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Veiculo.Placa)}", "Valor da placa já cadastrado no sistema."));

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

        return new CadastrarVeiculoResponse(veiculoResult.Value!.Id.EncodeWithSqids());
    }
}
