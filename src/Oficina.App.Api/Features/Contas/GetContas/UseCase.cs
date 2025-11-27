

using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Contas.GetContas;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetContasRequest, IEnumerable<GetContasResponse>>
{
    public async Task<Result<IEnumerable<GetContasResponse>>> Execute(
        GetContasRequest input,
        CancellationToken ct = default
    )
    {
        var contas = await fluentQuery.For<Conta>()
            .WithIncludes(x => x.Include(c => c.Lojas))
            .WithPredicate(x => x.Usuarios.Any(c => c.UserId == input.UserId))
            .FindAllAsync(ct);

        if (contas == null)
            return Result.Fail(Erro.NaoEncontrado("Loja não encontrada"));

        var response = contas.Select(x => new GetContasResponse(
            x.Id.EncodeWithSqids(),
            x.Nome,
            x.Lojas!.Select(loja => new GetContaLojaResponse(
                loja.Id.EncodeWithSqids(),
                loja.NomeFantasia,
                loja.RazaoSocial                
            )).ToList()
        ));

        return Result.Success(response);
    }
}
