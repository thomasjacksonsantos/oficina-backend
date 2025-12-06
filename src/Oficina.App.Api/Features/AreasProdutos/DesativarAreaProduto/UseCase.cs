



using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;
using Oficina.Domain.Aggregates.AreaProdutoAggregates;

namespace Oficina.App.Api.Features.AreasProdutos.DesativarAreaProduto;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<DesativarAreaProdutoRequest, DesativarAreaProdutoResponse>
{
    public async Task<Result<DesativarAreaProdutoResponse>> Execute(
        DesativarAreaProdutoRequest input,
        CancellationToken ct = default
    )
    {
        var areaProduto = await fluentQuery.For<AreaProduto>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithTracking()
            .FindFirstAsync(ct);

        if (areaProduto == null)
            return Result.Fail(Erro.ValorInvalido($"{nameof(AreaProduto)}.{nameof(input.Id)}", "Area do produto não encontrado."));

        areaProduto.Desativar();

        await unitOfWork.SaveChangesAsync(ct);

        return new DesativarAreaProdutoResponse("Area de produto desativada com sucesso!");
    }
}
