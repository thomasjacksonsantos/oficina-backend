



using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;
using Oficina.Domain.Aggregates.FornecedorAggregates;

namespace Oficina.App.Api.Features.Fornecedores.DesativarFornecedor;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<DesativarFornecedorRequest, DesativarFornecedorResponse>
{
    public async Task<Result<DesativarFornecedorResponse>> Execute(
        DesativarFornecedorRequest input,
        CancellationToken ct = default
    )
    {
        var fornecedor = await fluentQuery.For<Fornecedor>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithTracking()
            .FindFirstAsync(ct);

        if (fornecedor == null)
            return Result.Fail(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(input.Id)}", "Fornecedor não encontrado."));

        fornecedor.Desativar();

        await unitOfWork.SaveChangesAsync(ct);

        return new DesativarFornecedorResponse(
            "Fornecedor desativado com sucesso."
        );
    }
}
