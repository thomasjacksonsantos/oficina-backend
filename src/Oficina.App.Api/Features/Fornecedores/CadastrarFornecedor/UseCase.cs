


using Oficina.Domain.Aggregates.FornecedorAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Fornecedores.CadastrarFornecedor;

public sealed class UseCase(
    IRepository<Fornecedor> fornecedorRepository,
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<CadastrarFornecedorRequest, CadastrarFornecedorResponse>
{
    public async Task<Result<CadastrarFornecedorResponse>> Execute(
        CadastrarFornecedorRequest input,
        CancellationToken ct = default
    )
    {
        var fornecedorExists = await fluentQuery.For<Fornecedor>()
            .WithPredicate(x => x.Documento.Numero== input.Documento) 
            .CountAsync();

        if (fornecedorExists > 0)
            return Result.Fail(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(Fornecedor.Documento)}", "Valor do documento já cadastrado no sistema."));

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

       var contatos = input.Contatos.Select(c =>
        {
            var contatoResult = Contato.Criar(c.Numero, c.TipoTelefone);
            if (contatoResult.IsFailed)
                return Result.Fail(contatoResult.Errors!);
            return contatoResult;
        }).ToList();

        if (endereco.IsFailed)
            return Result.Fail(endereco.Errors);

        var fornecedorResult = Fornecedor.Criar(
            input.NomeFantasia,
            input.Site,
            input.InscricaoEstadual,
            input.InscricaoMunicipal,
            input.TipoConsumidor,
            input.Documento,
            input.EmailFornecedor,
            input.DataNascimento,
            endereco.Value!,
            contatos.Select(c => c.Value!).ToList()
        );

        if (fornecedorResult.IsFailed)
            return Result.Fail(fornecedorResult.Errors!);

        await fornecedorRepository.AddAsync(fornecedorResult.Value!);
        await unitOfWork.SaveChangesAsync(ct);

        return new CadastrarFornecedorResponse(fornecedorResult.Value!.Id.EncodeWithSqids());
    }
}
