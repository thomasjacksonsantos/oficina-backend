



using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.UnidadeProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.UnidadesProdutos.GetUnidadeProdutoById;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetUnidadeProdutoByIdRequest, GetUnidadeProdutoByIdResponse>
{
    public async Task<Result<GetUnidadeProdutoByIdResponse>> Execute(
        GetUnidadeProdutoByIdRequest input,
        CancellationToken ct = default
    )
    {
        var unidadeProduto = await fluentQuery.For<UnidadeProduto>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithIncludes(x => x.Include(c => c.UnidadeProdutoStatus))
            .FindFirstAsync(ct);
        
        if (unidadeProduto is null)
            return Erro.ValorInvalido("Unidade de Produto não encontrada.");

        return new GetUnidadeProdutoByIdResponse
        (
            unidadeProduto.Id.EncodeWithSqids(),
            unidadeProduto.Descricao,
            unidadeProduto.UnidadeProdutoStatus.Key,
            unidadeProduto.Criado.Valor,
            unidadeProduto.Atualizado.Valor
        );
    }
}