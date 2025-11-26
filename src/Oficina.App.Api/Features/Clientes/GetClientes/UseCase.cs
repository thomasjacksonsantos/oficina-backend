


using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Clientes.GetClientes;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetClientesRequest, PagedResult<GetClientesResponse>>
{
    public async Task<Result<PagedResult<GetClientesResponse>>> Execute(
        GetClientesRequest input,
        CancellationToken ct = default
    )
    {
        var clientes = await fluentQuery.For<Cliente>()
            .WithIncludes(c => c.Include(c => c.Sexo)
                                 .Include(c => c.TipoDocumento)
                                 .Include(c => c.ClienteStatus))
            .FindAllPagedAsync(input.Pagina, input.TotalPagina, ct);

        return clientes.MapTo(c => new GetClientesResponse(
            c.Id.EncodeWithSqids(),
            c.Nome,
            c.RazaoSocial,
            c.Sexo.Key,
            c.TipoDocumento.Key,
            c.Documento.Numero,
            c.Email.Valor,
            c.ClienteStatus.Key,
            c.DataNascimento.Valor,
            c.Contatos.Select(contact => new ContatoClientesResponse(
                contact.Numero,
                contact.TipoTelefone // ou .Nome, se existir
            )),
            new EnderecoClientesResponse(
                c.Endereco.Pais,
                c.Endereco.Estado,
                c.Endereco.Cidade,
                c.Endereco.Logradouro,
                c.Endereco.Bairro,
                c.Endereco.Complemento,
                c.Endereco.Cep.Valor,
                c.Endereco.Numero
            )
        ));
    }
}
