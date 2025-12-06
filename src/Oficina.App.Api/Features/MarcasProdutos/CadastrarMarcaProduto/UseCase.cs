

using Oficina.Domain.Aggregates.MarcaProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.MarcasProdutos.CadastrarMarcaProduto;

public sealed class UseCase(
    IRepository<MarcaProduto> marcaProdutoRepository,
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<CadastrarMarcaProdutoRequest, CadastrarMarcaProdutoResponse>
{
    public async Task<Result<CadastrarMarcaProdutoResponse>> Execute(
        CadastrarMarcaProdutoRequest input,
        CancellationToken ct = default
    )
    {
        var marcaProdutoExists = await fluentQuery.For<MarcaProduto>()
            .WithPredicate(x =>
                x.Descricao == input.Descricao
            )
            .CountAsync();

        if (marcaProdutoExists > 0)
            return Result.Fail(Erro.ValorInvalido($"{nameof(MarcaProduto)}", "Valor da descrição já consta cadastrado no sistema."));

        var marcaProduto = MarcaProduto.Criar(
            input.Descricao
        );

        if (marcaProduto.IsFailed)
            return Result.Fail(marcaProduto.Errors!);

        await marcaProdutoRepository.AddAsync(marcaProduto.Value!);
        await unitOfWork.SaveChangesAsync(ct);

        return new CadastrarMarcaProdutoResponse(marcaProduto.Value!.Id.EncodeWithSqids());
    }
}
