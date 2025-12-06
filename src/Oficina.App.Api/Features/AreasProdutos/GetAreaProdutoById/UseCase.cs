



using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.AreaProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.AreasProdutos.GetAreaProdutoById;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetAreaProdutoByIdRequest, GetAreaProdutoByIdResponse>
{
    public async Task<Result<GetAreaProdutoByIdResponse>> Execute(
        GetAreaProdutoByIdRequest input,
        CancellationToken ct = default
    )
    {
        var areaProduto = await fluentQuery.For<AreaProduto>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithIncludes(x => x.Include(c => c.AreaProdutoStatus))
            .FindFirstAsync(ct);
        
        if (areaProduto is null)
            return Erro.ValorInvalido("Area do Produto não encontrado.");

        return new GetAreaProdutoByIdResponse
        (
            areaProduto.Id.EncodeWithSqids(),
            areaProduto.Descricao,
            areaProduto.AreaProdutoStatus.Key,
            areaProduto.Criado.Valor,
            areaProduto.Atualizado.Valor
        );
    }
}