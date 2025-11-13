

using Oficina.Domain.Aggregates.OrdemServicoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.OrdemDeServico.GetAllOrdemDeServico;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetAllOrdemDeServicoRequest, PagedResult<GetAllOrdemDeServicoResponse>>
{
    public async Task<Result<PagedResult<GetAllOrdemDeServicoResponse>>> Execute(
        GetAllOrdemDeServicoRequest input,
        CancellationToken ct = default
    ) =>
        await fluentQuery
            .For<OrdemServico>()
            .WithPredicate(p => p.VeiculoCliente.Cliente.Documento.Numero.Contains(input.Documento))
            .WithProjection(p => new GetAllOrdemDeServicoResponse(
                p.Id,
                p.DataFaturamentoInicial,
                p.DataFaturamentoFinal,
                p.DataPrevisao,
                p.VeiculoCliente.Veiculo.Modelo,
                p.VeiculoCliente.Veiculo.Placa,
                p.VeiculoCliente.Cliente.Nome,
                p.VeiculoCliente.Cliente.Documento.Numero,
                p.Funcionario!.Nome,
                p.OrdemServicoStatus.Key
            ))
            .FindAllPagedAsync(input.Pagina, input.TotalPagina, ct);
}
