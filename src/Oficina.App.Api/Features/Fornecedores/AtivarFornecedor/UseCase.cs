



using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;
using Oficina.Domain.Aggregates.FornecedorAggregates;

namespace Oficina.App.Api.Features.Fornecedores.AtivarFornecedor;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtivarFornecedorRequest, AtivarFornecedorResponse>
{
    public async Task<Result<AtivarFornecedorResponse>> Execute(
        AtivarFornecedorRequest input,
        CancellationToken ct = default
    )
    {
        var fornecedor = await fluentQuery.For<Fornecedor>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithTracking()
            .FindFirstAsync(ct);

        if (fornecedor == null)
            return Result.Fail(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(input.Id)}", "Fornecedor não encontrado."));

        fornecedor.Ativar();

        await unitOfWork.SaveChangesAsync(ct);

        return new AtivarFornecedorResponse(
            "Fornecedor ativado com sucesso."
        );
    }
}
