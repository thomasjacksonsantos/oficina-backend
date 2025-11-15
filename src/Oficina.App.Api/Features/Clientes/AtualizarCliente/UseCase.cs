


using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Clientes.AtualizarCliente;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtualizarClienteRequest, AtualizarClienteResponse>
{
    public async Task<Result<AtualizarClienteResponse>> Execute(
        AtualizarClienteRequest input,
        CancellationToken ct = default
    )
    {
        var cliente = await fluentQuery
            .For<Cliente>()
            .WithPredicate(x => x.Id == input.Id)
            .WithIncludes(x => x.Include(c => c.Sexo))
            .WithIncludes(x => x.Include(c => c.TipoDocumento))
            .WithTracking()
            .FindFirstAsync(ct);

        if (cliente == null)
            return Result.Fail(Erro.Validacao(nameof(input.Id), nameof(input.Id), "Cliente não encontrado."));

        var contatos = input.Contatos.Select(c => Contato.Criar(c.Numero, c.TipoTelefone));
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
