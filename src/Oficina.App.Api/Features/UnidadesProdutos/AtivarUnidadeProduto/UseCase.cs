



using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;
using Oficina.Domain.Aggregates.UnidadeProdutoAggregates;

namespace Oficina.App.Api.Features.UnidadesProdutos.AtivarUnidadeProduto;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtivarUnidadeProdutoRequest, AtivarUnidadeProdutoResponse>
{
    public async Task<Result<AtivarUnidadeProdutoResponse>> Execute(
        AtivarUnidadeProdutoRequest input,
        CancellationToken ct = default
    )
    {
        var unidadeProduto = await fluentQuery.For<UnidadeProduto>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithTracking()
            .FindFirstAsync(ct);

        if (unidadeProduto == null)
            return Result.Fail(Erro.ValorInvalido($"{nameof(UnidadeProduto)}.{nameof(input.Id)}", "Unidade de produto não encontrada."));

        unidadeProduto.Ativar();

        await unitOfWork.SaveChangesAsync(ct);

        return new AtivarUnidadeProdutoResponse("Unidade de produto ativada com sucesso!");
    }
}
