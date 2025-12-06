



using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;
using Oficina.Domain.Aggregates.GrupoProdutoAggregates;

namespace Oficina.App.Api.Features.GruposProdutos.AtivarGrupoProduto;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtivarGrupoProdutoRequest, AtivarGrupoProdutoResponse>
{
    public async Task<Result<AtivarGrupoProdutoResponse>> Execute(
        AtivarGrupoProdutoRequest input,
        CancellationToken ct = default
    )
    {
        var grupoProduto = await fluentQuery.For<GrupoProduto>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithTracking()
            .FindFirstAsync(ct);

        if (grupoProduto == null)
            return Result.Fail(Erro.ValorInvalido($"{nameof(GrupoProduto)}.{nameof(input.Id)}", "Grupo de produto não encontrado."));

        grupoProduto.Ativar();

        await unitOfWork.SaveChangesAsync(ct);

        return new AtivarGrupoProdutoResponse("Grupo de produto ativado com sucesso!");
    }
}
