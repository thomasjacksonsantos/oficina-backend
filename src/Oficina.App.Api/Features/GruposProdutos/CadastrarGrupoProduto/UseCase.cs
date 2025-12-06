


using Oficina.Domain.Aggregates.GrupoProdutoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.GruposProdutos.CadastrarGrupoProduto;

public sealed class UseCase(
    IRepository<GrupoProduto> grupoProdutoRepository,
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<CadastrarGrupoProdutoRequest, CadastrarGrupoProdutoResponse>
{
    public async Task<Result<CadastrarGrupoProdutoResponse>> Execute(
        CadastrarGrupoProdutoRequest input,
        CancellationToken ct = default
    )
    {
        var grupoProdutoExists = await fluentQuery.For<GrupoProduto>()
            .WithPredicate(x =>
                x.ANP == input.ANP &&
                x.NCM == input.NCM &&
                x.Area == input.Area &&
                x.Descricao == input.Descricao
            )
            .CountAsync();

        if (grupoProdutoExists > 0)
            return Result.Fail(Erro.ValorInvalido($"{nameof(GrupoProduto)}", "Valor do documento já cadastrado no sistema."));

        var grupoProduto = GrupoProduto.Criar(
            input.Descricao,
            input.Area,
            input.NCM,
            input.ANP
        );

        if (grupoProduto.IsFailed)
            return Result.Fail(grupoProduto.Errors!);

        await grupoProdutoRepository.AddAsync(grupoProduto.Value!);
        await unitOfWork.SaveChangesAsync(ct);

        return new CadastrarGrupoProdutoResponse(grupoProduto.Value!.Id.EncodeWithSqids());
    }
}
