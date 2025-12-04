


using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.FornecedorAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Fornecedores.GetFornecedorById;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetFornecedorByIdRequest, GetFornecedorByIdResponse>
{
    public async Task<Result<GetFornecedorByIdResponse>> Execute(
        GetFornecedorByIdRequest input,
        CancellationToken ct = default
    )
    {
        var fornecedor = await fluentQuery.For<Fornecedor>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithIncludes(x => x.Include(c => c.TipoConsumidor).Include(c => c.FornecedorStatus))
            .FindFirstAsync(ct);

        if (fornecedor == null)
            return Result.Fail(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(input.Id)}", "Fornecedor não encontrado."));


        return new GetFornecedorByIdResponse(
            fornecedor.Id.EncodeWithSqids(),
            fornecedor.NomeFantasia,
            fornecedor.Site,
            fornecedor.InscricaoEstadual,
            fornecedor.InscricaoMunicipal,
            fornecedor.TipoConsumidor.Key,
            fornecedor.Documento.Numero,
            fornecedor.Email.Valor,
            fornecedor.FornecedorStatus.Key,
            fornecedor.DataNascimento.Valor,
            new GetFornecedorEndereco(
                fornecedor.Endereco.Pais,
                fornecedor.Endereco.Estado,
                fornecedor.Endereco.Cidade,
                fornecedor.Endereco.Logradouro,
                fornecedor.Endereco.Bairro,
                fornecedor.Endereco.Complemento,
                fornecedor.Endereco.Cep.Valor,
                fornecedor.Endereco.Numero
            ),
            fornecedor.Contatos!.Select(c => new GetFornecedorContato(
                c.Numero,
                c.TipoTelefone.ToString()
            )).ToList()
        );
    }
}