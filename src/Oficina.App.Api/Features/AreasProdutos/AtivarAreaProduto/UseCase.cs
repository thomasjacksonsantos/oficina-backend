



using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;
using Oficina.Domain.Aggregates.AreaProdutoAggregates;

namespace Oficina.App.Api.Features.AreasProdutos.AtivarAreaProduto;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtivarAreaProdutoRequest, AtivarAreaProdutoResponse>
{
    public async Task<Result<AtivarAreaProdutoResponse>> Execute(
        AtivarAreaProdutoRequest input,
        CancellationToken ct = default
    )
    {
        var areaProduto = await fluentQuery.For<AreaProduto>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithTracking()
            .FindFirstAsync(ct);

        if (areaProduto == null)
            return Result.Fail(Erro.ValorInvalido($"{nameof(AreaProduto)}.{nameof(input.Id)}", "Área de produto não encontrada."));

        areaProduto.Ativar();

        await unitOfWork.SaveChangesAsync(ct);

        return new AtivarAreaProdutoResponse("Área de produto ativada com sucesso!");
    }
}
