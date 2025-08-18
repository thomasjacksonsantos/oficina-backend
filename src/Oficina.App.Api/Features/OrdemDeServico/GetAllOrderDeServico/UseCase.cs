

using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.OrdemDeServico.GetAllOrdemDeServico;

public sealed class UseCase(
    // IRepository<Cliente> clienteRepository
)
    : IUseCase<GetAllOrdemDeServicoRequest, GetAllOrdemDeServicoResponse>
{
    public Task<Result<GetAllOrdemDeServicoResponse>> Execute(
        GetAllOrdemDeServicoRequest input,
        CancellationToken ct = default
    )
    {
        throw new NotImplementedException();
        
        // var clientes = await clienteRepository.FindAllByPredicate(
        //     predicate: p => p.Documento.Numero.Contains(input.Documento),
        //     projection: p => new GetClienteResponse()
        //     pagination: new Pagination(input.Pagina <= 0 ? 1 : input.Pagina, input.TotalPagina <= 0 ? 20 : input.TotalPagina),
        //     includes: p => p.Include(c => c.VeiculoClientes).ThenInclude(c => c.Veiculo).Include(c => c.VeiculoClientes).ThenInclude(c => c.Cliente),
        //     ct: ct
        // );
    }
}
