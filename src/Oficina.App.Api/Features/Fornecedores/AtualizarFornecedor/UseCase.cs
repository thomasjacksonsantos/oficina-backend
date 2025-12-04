



using Oficina.Domain.Aggregates.FornecedorAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Fornecedores.AtualizarFornecedor;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtualizarFornecedorRequest, AtualizarFornecedorResponse>
{
    public async Task<Result<AtualizarFornecedorResponse>> Execute(
        AtualizarFornecedorRequest input,
        CancellationToken ct = default
    )
    {
        var fornecedor = await fluentQuery.For<Fornecedor>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithTracking()
            .FindFirstAsync(ct);

        if (fornecedor == null)
            return Result.Fail(Erro.ValorInvalido($"{nameof(Fornecedor)}.{nameof(input.Id)}", "Fornecedor não encontrado."));

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

        fornecedor.Atualizar(
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

        await unitOfWork.SaveChangesAsync(ct);

        return new AtualizarFornecedorResponse(
            "Fornecedor atualizado com sucesso."
        );
    }
}
