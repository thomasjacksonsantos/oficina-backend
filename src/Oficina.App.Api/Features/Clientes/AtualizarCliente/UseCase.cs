


using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Clientes.AtualizarCliente;

public sealed class UseCase(
    IRepository<Cliente> clienteRepository,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtualizarClienteRequest, AtualizarClienteResponse>
{
    public async Task<Result<AtualizarClienteResponse>> Execute(
        AtualizarClienteRequest input,
        CancellationToken ct = default
    )
    {
        var cliente = await clienteRepository.FindFirstByPredicate(
            predicate: c => c.Id == input.Id,
            includes: c => c.Include(c => c.Sexo).Include(c => c.TipoDocumento),
            asNoTracking: false,
            ct
        ) ?? null!;

        if (cliente == null)
            return Result.Fail("Cliente não encontrado.");

        var contatos = input.Contatos.Select(c => Contato.Criar(c.DDD, c.Numero, c.TipoTelefone));
        if (contatos.Any(c => c.IsFailed))
            return Result.Fail(contatos.SelectMany(c => c.Errors!).ToList());

        var endereco = Endereco.Criar(
            input.Endereco.Pais,
            input.Endereco.Estado,
            input.Endereco.Cidade,
            input.Endereco.Logradouro,
            input.Endereco.Bairro,
            input.Endereco.Complemento,
            input.Endereco.Cep,
            input.Endereco.Numero
        );

        if (endereco.IsFailed)
            return Result.Fail(endereco.Errors!);

        var clienteResult = cliente.Atualizar(
            input.Nome,
            input.Sexo,
            input.Documento,
            input.EmailCliente,
            input.DataNascimento,
            contatos.Select(c => c.Value!).ToList(),
            endereco.Value!
        );

        if (clienteResult.IsFailed)
            return Result.Fail(clienteResult.Errors!);

        await unitOfWork.SaveChangesAsync(ct);

        return new AtualizarClienteResponse();
    }
}
