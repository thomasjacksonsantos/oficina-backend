


using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Clientes.GetClienteById;

public sealed class UseCase(
    IRepository<Cliente> clienteRepository
)
    : IUseCase<GetClienteByIdRequest, GetClienteByIdResponse>
{
    public async Task<Result<GetClienteByIdResponse>> Execute(
        GetClienteByIdRequest input,
        CancellationToken ct = default
    )
    {
        var cliente = await clienteRepository.FindFirstByPredicate(
            predicate: c => c.Id == input.Id,
            includes: c => c.Include(c => c.Sexo).Include(c => c.TipoDocumento),
            asNoTracking: true,
            ct
        ) ?? null!;


        return new GetClienteByIdResponse(
            cliente.Nome,
            cliente.RazaoSocial,
            cliente.Sexo.Key,
            cliente.TipoDocumento.Key,
            cliente.Documento.Numero,
            cliente.Email.Valor,
            cliente.DataNascimento.Valor,
            cliente.Contatos.Select(c => new ContatoClienteResponse(
                c.Numero,
                c.TipoTelefone.ToString()
            )).ToList(),
            new EnderecoClienteResponse(
                cliente.Endereco.Pais!,
                cliente.Endereco.Estado!,
                cliente.Endereco.Cidade!,
                cliente.Endereco.Logradouro!,
                cliente.Endereco.Numero!,
                cliente.Endereco.Complemento!,
                cliente.Endereco.Cep.Valor,
                cliente.Endereco.Numero
            )
        );
    }
}
