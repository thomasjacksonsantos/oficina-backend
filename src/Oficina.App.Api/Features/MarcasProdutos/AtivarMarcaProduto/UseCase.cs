



using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;
using Oficina.Domain.Aggregates.MarcaProdutoAggregates;

namespace Oficina.App.Api.Features.MarcasProdutos.AtivarMarcaProduto;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtivarMarcaProdutoRequest, AtivarMarcaProdutoResponse>
{
    public async Task<Result<AtivarMarcaProdutoResponse>> Execute(
        AtivarMarcaProdutoRequest input,
        CancellationToken ct = default
    )
    {
        var marcaProduto = await fluentQuery.For<MarcaProduto>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithTracking()
            .FindFirstAsync(ct);

        if (marcaProduto == null)
            return Result.Fail(Erro.ValorInvalido($"{nameof(MarcaProduto)}.{nameof(input.Id)}", "Marca de produto não encontrada."));

        marcaProduto.Ativar();

        await unitOfWork.SaveChangesAsync(ct);

        return new AtivarMarcaProdutoResponse("Marca de produto ativada com sucesso!");
    }
}
