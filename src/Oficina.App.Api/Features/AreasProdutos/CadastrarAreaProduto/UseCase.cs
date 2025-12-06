


using Oficina.Domain.Aggregates.AreaProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.AreasProdutos.CadastrarAreaProduto;

public sealed class UseCase(
    IRepository<AreaProduto> areaProdutoRepository,
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<CadastrarAreaProdutoRequest, CadastrarAreaProdutoResponse>
{
    public async Task<Result<CadastrarAreaProdutoResponse>> Execute(
        CadastrarAreaProdutoRequest input,
        CancellationToken ct = default
    )
    {
        var areaProdutoExists = await fluentQuery.For<AreaProduto>()
            .WithPredicate(x =>
                x.Descricao == input.Descricao &&
                x.Garantia == input.Garantia
            )
            .CountAsync();

        if (areaProdutoExists > 0)
            return Result.Fail(Erro.ValorInvalido($"{nameof(AreaProduto)}", "Area do propduto já se encontra cadastrado no sistema."));

        var areaProduto = AreaProduto.Criar(
            input.Descricao,
            input.Garantia
        );

        if (areaProduto.IsFailed)
            return Result.Fail(areaProduto.Errors!);

        await areaProdutoRepository.AddAsync(areaProduto.Value!);
        await unitOfWork.SaveChangesAsync(ct);

        return new CadastrarAreaProdutoResponse(areaProduto.Value!.Id.EncodeWithSqids());
    }
}
