



using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.GrupoProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.GruposProdutos.GetGruposProdutos;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetGruposProdutosRequest, PagedResult<GetGruposProdutosResponse>>
{
    public async Task<Result<PagedResult<GetGruposProdutosResponse>>> Execute(
        GetGruposProdutosRequest input,
        CancellationToken ct = default
    )
    {
        var fornecedores = await fluentQuery.For<GrupoProduto>()
            .WithIncludes(x => x.Include(c => c.GrupoProdutoStatus))
            .FindAllPagedAsync(input.Pagina, input.TotalPagina, ct);

        return fornecedores.MapTo(x => new GetGruposProdutosResponse
        (
            x.Id.EncodeWithSqids(),
            x.Descricao,
            x.Area,
            x.NCM,
            x.ANP,
            x.GrupoProdutoStatus.Key,
            x.Criado.Valor,
            x.Atualizado.Valor
        ));
    }
}