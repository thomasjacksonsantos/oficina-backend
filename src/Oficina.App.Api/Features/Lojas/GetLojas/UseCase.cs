

using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.Aggregates.LojaAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Lojas.GetLojas;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetLojasRequest, IEnumerable<GetLojasResponse>>
{
    public async Task<Result<IEnumerable<GetLojasResponse>>> Execute(
        GetLojasRequest input,
        CancellationToken ct = default
    )
    {
        var conta = await fluentQuery.For<Conta>()
            .WithPredicate(x => x.Id == input.ContaId.DecodeWithSqids())
            .WithIncludes(x => x.Include(c => c.Lojas))
            .FindFirstAsync(ct);
        if (conta == null)
            return Result.Fail(Erro.NaoEncontrado("Conta não encontrada"));



        var lojas = conta.Lojas!.Select(loja => new GetLojasResponse(
            loja.Id.EncodeWithSqids(),
            loja.NomeFantasia,
            loja.RazaoSocial,
            loja.Documento.Numero,
            loja.InscricaoEstadual,
            loja.InscricaoMunicipal,
            loja.Site,
            loja.LogoTipo,
            loja.Endereco != null ? new GetLojasEnderecoResponse(
                loja.Endereco.Pais,
                loja.Endereco.Estado,
                loja.Endereco.Cidade,
                loja.Endereco.Logradouro,
                loja.Endereco.Bairro,
                loja.Endereco.Complemento,
                loja.Endereco.Cep.Valor,
                loja.Endereco.Numero
            ) : null!,
            loja.Contatos != null ? loja.Contatos.Select(contato => new GetLojasContatoResponse(
                contato.Numero,
                contato.TipoTelefone.ToString()
            )).ToList() : new List<GetLojasContatoResponse>(
        )));

        return Result.Success(lojas!);
    }
}
