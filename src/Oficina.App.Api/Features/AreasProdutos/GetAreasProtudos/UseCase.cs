



using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.AreaProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.AreasProdutos.GetAreasProdutos;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetAreasProdutosRequest, PagedResult<GetAreasProdutosResponse>>
{
    public async Task<Result<PagedResult<GetAreasProdutosResponse>>> Execute(
        GetAreasProdutosRequest input,
        CancellationToken ct = default
    )
    {
        var fornecedores = await fluentQuery.For<AreaProduto>()
            .WithIncludes(x => x.Include(c => c.AreaProdutoStatus))
            .FindAllPagedAsync(input.Pagina, input.TotalPagina, ct);

        return fornecedores.MapTo(x => new GetAreasProdutosResponse
        (
            x.Id.EncodeWithSqids(),
            x.Descricao,
            x.Garantia,
            x.AreaProdutoStatus.Key,
            x.Criado.Valor,
            x.Atualizado.Valor
        ));
    }
}