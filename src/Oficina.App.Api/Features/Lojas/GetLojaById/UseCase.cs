

using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.ContaAggregates;
using Oficina.Domain.Aggregates.LojaAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Lojas.GetLojaById;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetLojaByIdRequest, GetLojaByIdResponse>
{
    public async Task<Result<GetLojaByIdResponse>> Execute(
        GetLojaByIdRequest input,
        CancellationToken ct = default
    )
    {
        var loja = await fluentQuery.For<Loja>()
            .WithPredicate(x => x.Id == input.Id.DecodeWithSqids())
            .FindFirstAsync(ct);

        if (loja == null)
            return Result.Fail(Erro.NaoEncontrado("Loja não encontrada"));

        var response = new GetLojaByIdResponse(
            loja.Id.EncodeWithSqids(),
            loja.NomeFantasia,
            loja.RazaoSocial,
            loja.Documento.Numero,
            loja.InscricaoEstadual,
            loja.InscricaoMunicipal,
            loja.Site,
            loja.LogoTipo,
            loja.Endereco != null ? new GetLojaByIdEnderecoResponse(
                loja.Endereco.Pais,
                loja.Endereco.Estado,
                loja.Endereco.Cidade,
                loja.Endereco.Logradouro,
                loja.Endereco.Bairro,
                loja.Endereco.Complemento,
                loja.Endereco.Cep.Valor,
                loja.Endereco.Numero
            ) : null!,
            loja.Contatos != null ? loja.Contatos.Select(contato => new GetLojaByIdContatoResponse(
                contato.Numero,
                contato.TipoTelefone.ToString()
            )).ToList() : new List<GetLojaByIdContatoResponse>(
        ));

        return Result.Success(response);
    }
}
