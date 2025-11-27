

using Oficina.Domain.Aggregates.LojaAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Lojas.AtualizarLoja;

public sealed class UseCase(
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<AtualizarLojaRequest, AtualizarLojaResponse>
{
    public async Task<Result<AtualizarLojaResponse>> Execute(
        AtualizarLojaRequest input,
        CancellationToken ct = default
    )
    {
        var loja = await fluentQuery.For<Loja>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .WithTracking()
            .FindFirstAsync(ct);
        if (loja == null)
            return Result.Fail(Erro.NaoEncontrado("Loja não encontrada"));

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

        input.Contatos.Select(c =>
        {
            var contatoResult = Contato.Criar(c.Numero, c.TipoTelefone);
            if (contatoResult.IsFailed)
                return Result.Fail(contatoResult.Errors!);
            return contatoResult;
        }).ToList();

        if (endereco.IsFailed)
            return Result.Fail(endereco.Errors);

        var result = loja.Atualizar(
            input.NomeFantasia,
            input.RazaoSocial,
            input.InscricaoEstadual,
            input.InscricaoMunicipal,
            input.Site,
            input.LogoTipo,
            input.Documento,
            endereco.Value!,
            input.Contatos.Select(c => Contato.Criar(c.Numero, c.TipoTelefone).Value!).ToList()
        );

        if (result.IsFailed)
            return Result.Fail(result.Errors);

        await unitOfWork.SaveChangesAsync(ct);

        return Result.Success(new AtualizarLojaResponse(
            "Loja atualizada com sucesso"
        ));

    }
}
