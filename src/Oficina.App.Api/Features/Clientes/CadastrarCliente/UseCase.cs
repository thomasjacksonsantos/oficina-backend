


using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.Diagnostics;
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
        var result = new Result<CadastrarClienteResponse>();

        var contatos = input.Contatos.Select(c => Contato.Criar(c.Numero, c.TipoTelefone));
        if (contatos.Any(c => c.IsFailed))
            result.WithErrors(contatos!.SelectMany(c => c.Errors!)!.ToList());

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
            result.WithErrors(endereco.Errors!);

        var clienteResult = Cliente.Criar(
            input.Nome,
            input.RazaoSocial,
            input.Sexo,
            input.Documento,
            input.EmailCliente,
            input.DataNascimento,
            contatos.Select(c => c.Value!).ToList(),
            endereco.Value!
        );

        if (clienteResult.IsFailed)
            result.WithErrors(clienteResult.Errors!);

        if (result.IsFailed)
            return result;

        await clienteRepository.AddAsync(clienteResult.Value!);
        await unitOfWork.SaveChangesAsync(ct);

        return new CadastrarClienteResponse(clienteResult.Value!.Id);
    }
}
