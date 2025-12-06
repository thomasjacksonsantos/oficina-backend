



using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.MarcaProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.MarcasProdutos.GetMarcaProdutoById;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetMarcaProdutoByIdRequest, GetMarcaProdutoByIdResponse>
{
    public async Task<Result<GetMarcaProdutoByIdResponse>> Execute(
        GetMarcaProdutoByIdRequest input,
        CancellationToken ct = default
    )
    {
        var marcaProduto = await fluentQuery.For<MarcaProduto>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithIncludes(x => x.Include(c => c.MarcaProdutoStatus))
            .FindFirstAsync(ct);
        
        if (marcaProduto is null)
            return Erro.ValorInvalido("Marca de Produto não encontrada.");

        return new GetMarcaProdutoByIdResponse
        (
            marcaProduto.Id.EncodeWithSqids(),
            marcaProduto.Descricao,
            marcaProduto.MarcaProdutoStatus.Key,
            marcaProduto.Criado.Valor,
            marcaProduto.Atualizado.Valor
        );
    }
}