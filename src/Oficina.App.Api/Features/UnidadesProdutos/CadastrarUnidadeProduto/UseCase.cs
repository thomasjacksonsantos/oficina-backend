

using Oficina.Domain.Aggregates.UnidadeProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.UnidadesProdutos.CadastrarUnidadeProduto;

public sealed class UseCase(
    IRepository<UnidadeProduto> unidadeProdutoRepository,
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<CadastrarUnidadeProdutoRequest, CadastrarUnidadeProdutoResponse>
{
    public async Task<Result<CadastrarUnidadeProdutoResponse>> Execute(
        CadastrarUnidadeProdutoRequest input,
        CancellationToken ct = default
    )
    {
        var unidadeProdutoExists = await fluentQuery.For<UnidadeProduto>()
            .WithPredicate(x =>
                x.Descricao == input.Descricao
            )
            .CountAsync();

        if (unidadeProdutoExists > 0)
            return Result.Fail(Erro.ValorInvalido($"{nameof(UnidadeProduto)}", "Valor do documento já cadastrado no sistema."));

        var unidadeProduto = UnidadeProduto.Criar(
            input.Descricao
        );

        if (unidadeProduto.IsFailed)
            return Result.Fail(unidadeProduto.Errors!);

        await unidadeProdutoRepository.AddAsync(unidadeProduto.Value!);
        await unitOfWork.SaveChangesAsync(ct);

        return new CadastrarUnidadeProdutoResponse(unidadeProduto.Value!.Id.EncodeWithSqids());
    }
}
