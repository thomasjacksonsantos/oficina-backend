
using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.OrdemServicoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.OrdemDeServico.GetOrdemDeServicoById;

public sealed class UseCase(
    IRepository<OrdemServico> ordemServicoRepository    
)
    : IUseCase<GetOrdemDeServicoByIdRequest, GetOrdemDeServicoByIdResponse>
{
    public async Task<Result<GetOrdemDeServicoByIdResponse>> Execute(
        GetOrdemDeServicoByIdRequest input,
        CancellationToken ct = default
    )
    {
        var ordemServico = await ordemServicoRepository.FindFirstByPredicate(
            predicate: c => c.Id == input.Id,
            projection: c => new GetOrdemDeServicoByIdResponse(),
            includes: c =>
                c.Include(c => c.FuncionarioExecutor)
                .Include(c => c.Pagamento)
                .Include(c => c.VeiculoCliente)
                    .ThenInclude(c => c.Veiculo)
                .Include(c => c.VeiculoCliente)
                    .ThenInclude(c => c.Cliente)
                .Include(c => c.Items)
        );

        return ordemServico ?? null!;
    }
}
