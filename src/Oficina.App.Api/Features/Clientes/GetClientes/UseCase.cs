


using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Clientes.GetClientes;

public sealed class UseCase(
    IRepository<Cliente> clienteRepository
)
    : IUseCase<GetClientesRequest, PagedResult<GetClientesResponse>>
{
    public async Task<Result<PagedResult<GetClientesResponse>>> Execute(
        GetClientesRequest input,
        CancellationToken ct = default
    )
    {
        return await clienteRepository.FindAllByPredicate(
            predicate: p => input.Documento == null || p.Documento.Numero.Contains(input.Documento),
            projection: p => new GetClientesResponse(
                p.Id,
                p.Nome,
                p.Documento.Numero
            ),
            pagination: new Pagination(input.Pagina <= 0 ? 1 : input.Pagina, input.TotalPagina <= 0 ? 20 : input.TotalPagina),
            ct: ct
        );
    }
}
