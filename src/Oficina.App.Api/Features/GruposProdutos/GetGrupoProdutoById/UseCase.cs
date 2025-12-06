



using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.GrupoProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.GruposProdutos.GetGrupoProdutoById;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetGrupoProdutoByIdRequest, GetGrupoProdutoByIdResponse>
{
    public async Task<Result<GetGrupoProdutoByIdResponse>> Execute(
        GetGrupoProdutoByIdRequest input,
        CancellationToken ct = default
    )
    {
        var grupoProduto = await fluentQuery.For<GrupoProduto>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithIncludes(x => x.Include(c => c.GrupoProdutoStatus))
            .FindFirstAsync(ct);
        
        if (grupoProduto is null)
            return Erro.ValorInvalido("Grupo de Produto não encontrado.");

        return new GetGrupoProdutoByIdResponse
        (
            grupoProduto.Id.EncodeWithSqids(),
            grupoProduto.Descricao,
            grupoProduto.Area,
            grupoProduto.NCM,
            grupoProduto.ANP,
            grupoProduto.GrupoProdutoStatus.Key,
            grupoProduto.Criado.Valor,
            grupoProduto.Atualizado.Valor
        );
    }
}