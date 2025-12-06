



using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.UnidadeProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.UnidadesProdutos.GetUnidadesProdutos;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetUnidadesProdutosRequest, PagedResult<GetUnidadesProdutosResponse>>
{
    public async Task<Result<PagedResult<GetUnidadesProdutosResponse>>> Execute(
        GetUnidadesProdutosRequest input,
        CancellationToken ct = default
    )
    {
        var fornecedores = await fluentQuery.For<UnidadeProduto>()
            .WithIncludes(x => x.Include(c => c.UnidadeProdutoStatus))
            .FindAllPagedAsync(input.Pagina, input.TotalPagina, ct);

        return fornecedores.MapTo(x => new GetUnidadesProdutosResponse
        (
            x.Id.EncodeWithSqids(),
            x.Descricao,
            x.UnidadeProdutoStatus.Key,
            x.Criado.Valor,
            x.Atualizado.Valor
        ));
    }
}