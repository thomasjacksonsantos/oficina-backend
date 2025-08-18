

using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Clientes.CadastrarCliente;

public sealed class UseCase(
    IRepository<Cliente> clienteRepository,
    IUnitOfWork unitOfWork
)
    : IUseCase<CadastrarClienteRequest, CadastrarClienteResponse>
{
    public async Task<Result<CadastrarClienteResponse>> Execute(
        CadastrarClienteRequest input,
        CancellationToken ct = default
    )
    {
        var contatos = input.Contatos.Select(c => Contato.Criar(c.DDD, c.Numero, c.TipoTelefone));
        if (contatos.Any(c => c.IsFailed))
            return Result.Fail(contatos.SelectMany(c => c.Errors!).ToList());

        var enderecos = input.Enderecos.Select(c =>
            Endereco.Criar(c.Pais, c.Estado, c.Cidade, c.Logradouro, c.Bairro, c.Complemento, c.Cep, c.Numero));
        if (enderecos.Any(c => c.IsFailed))
            return Result.Fail(enderecos.SelectMany(c => c.Errors!).ToList());

        var clienteResult = Cliente.Criar(
            input.Nome,
            input.Sexo,
            input.Documento,
            input.EmailCliente,
            input.DataNascimento,
            contatos.Select(c => c.Value!).ToList(),
            enderecos.Select(c => c.Value!).ToList()
        );

        if (clienteResult.IsFailed)
            return Result.Fail(clienteResult.Errors!);

        await clienteRepository.AddAsync(clienteResult.Value!);
        await unitOfWork.SaveChangesAsync(ct);

        return new CadastrarClienteResponse(clienteResult.Value!.Id);
    }
}
