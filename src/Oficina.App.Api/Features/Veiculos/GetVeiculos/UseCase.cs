



using Oficina.Domain.Aggregates.VeiculoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Veiculos.GetVeiculos;

public sealed class UseCase(
    IRepository<Veiculo> veiculoRepository
)
    : IUseCase<GetVeiculosRequest, PagedResult<GetVeiculosResponse>>
{
    public async Task<Result<PagedResult<GetVeiculosResponse>>> Execute(
        GetVeiculosRequest input,
        CancellationToken ct = default
    )
    {

        return await veiculoRepository.FindAllByPredicate(
            predicate: c => 0 == 0,
            projection: c => new GetVeiculosResponse(
                c.Placa,
                c.Modelo,
                c.Hodrometro,
                c.Cor,
                c.Ano,
                c.NumeroChassi,
                c.NumeroSerie,
                c.Motorizacao,
                c.Chassi
            ),
            pagination: new Pagination(input.Pagina <= 0 ? 1 : input.Pagina, input.TotalPagina <= 0 ? 20 : input.TotalPagina),            
            ct: ct
        ) ?? null!;       
    }
}