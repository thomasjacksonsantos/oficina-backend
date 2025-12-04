



using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.FornecedorAggregates;
using Oficina.Domain.Aggregates.VeiculoAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Fornecedores.GetFornecedores;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetFornecedoresRequest, PagedResult<GetFornecedoresResponse>>
{
    public async Task<Result<PagedResult<GetFornecedoresResponse>>> Execute(
        GetFornecedoresRequest input,
        CancellationToken ct = default
    )
    {
        var fornecedores = await fluentQuery.For<Fornecedor>()
            .WithIncludes(x => x.Include(c => c.FornecedorStatus).Include(c => c.TipoConsumidor))
            .FindAllPagedAsync(input.Pagina, input.TotalPagina, ct);

        return fornecedores.MapTo(x => new GetFornecedoresResponse
        (
            x.Id.EncodeWithSqids(),
            x.NomeFantasia,
            x.Documento.Numero,
            x.Contatos!.Select(c => new GetFornecedoresContato(
                c.Numero,
                c.TipoTelefone.ToString()
            )).ToList(),
            x.FornecedorStatus.Key
        ));
    }
}