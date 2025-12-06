



using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.MarcaProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.MarcasProdutos.GetMarcasProdutos;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetMarcasProdutosRequest, PagedResult<GetMarcasProdutosResponse>>
{
    public async Task<Result<PagedResult<GetMarcasProdutosResponse>>> Execute(
        GetMarcasProdutosRequest input,
        CancellationToken ct = default
    )
    {
        var fornecedores = await fluentQuery.For<MarcaProduto>()
            .WithIncludes(x => x.Include(c => c.MarcaProdutoStatus))
            .FindAllPagedAsync(input.Pagina, input.TotalPagina, ct);

        return fornecedores.MapTo(x => new GetMarcasProdutosResponse
        (
            x.Id.EncodeWithSqids(),
            x.Descricao,
            x.MarcaProdutoStatus.Key,
            x.Criado.Valor,
            x.Atualizado.Valor
        ));
    }
}