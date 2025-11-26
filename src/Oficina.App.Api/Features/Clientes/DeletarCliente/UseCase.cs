


using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Clientes.DeletarCliente;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<DeletarClienteRequest, DeletarClienteResponse>
{
    public async Task<Result<DeletarClienteResponse>> Execute(
        DeletarClienteRequest input,
        CancellationToken ct = default
    )
    {
        var cliente = await fluentQuery
            .For<Cliente>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithIncludes(x => x.Include(c => c.Sexo))
            .WithIncludes(x => x.Include(c => c.TipoDocumento))
            .WithTracking()
            .FindFirstAsync(ct);

        if (cliente == null)
            return Result.Fail(Erro.Validacao(nameof(input.Id), nameof(input.Id), "Cliente não encontrado."));

        cliente.Inativar();

        await unitOfWork.SaveChangesAsync(ct);

        return new DeletarClienteResponse();
    }
}
