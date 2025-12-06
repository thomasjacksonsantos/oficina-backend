



using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;
using Oficina.Domain.Aggregates.GrupoProdutoAggregates;

namespace Oficina.App.Api.Features.GruposProdutos.DesativarGrupoProduto;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<DesativarGrupoProdutoRequest, DesativarGrupoProdutoResponse>
{
    public async Task<Result<DesativarGrupoProdutoResponse>> Execute(
        DesativarGrupoProdutoRequest input,
        CancellationToken ct = default
    )
    {
        var grupoProduto = await fluentQuery.For<GrupoProduto>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithTracking()
            .FindFirstAsync(ct);

        if (grupoProduto == null)
            return Result.Fail(Erro.ValorInvalido($"{nameof(GrupoProduto)}.{nameof(input.Id)}", "Grupo de produto não encontrado."));

        grupoProduto.Desativar();

        await unitOfWork.SaveChangesAsync(ct);

        return new DesativarGrupoProdutoResponse("Grupo de produto desativado com sucesso!");
    }
}
