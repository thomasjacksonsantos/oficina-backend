


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

        var clienteResult = Cliente.Criar(
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

        await clienteRepository.AddAsync(clienteResult.Value!);
        await unitOfWork.SaveChangesAsync(ct);

        return new CadastrarClienteResponse(clienteResult.Value!.Id);
    }
}
