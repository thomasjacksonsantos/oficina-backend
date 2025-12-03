

using Microsoft.EntityFrameworkCore;
using Oficina.Domain.Aggregates.LojaAggregates;
using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.Lojas.GetLojaByContext;

public sealed class UseCase(
    IFluentQuery fluentQuery
)
    : IUseCase<GetLojaByContextRequest, GetLojaByContextResponse>
{
    public async Task<Result<GetLojaByContextResponse>> Execute(
        GetLojaByContextRequest input,
        CancellationToken ct = default
    )
    {
        var usuarioContexto = await fluentQuery.For<UsuarioContexto>()
            .WithPredicate(x => x.Usuario!.UserId == input.UserId)            
            .WithIncludes(x => 
                x.Include(c => c.Loja))
            .FindFirstAsync(ct);

        var loja = await fluentQuery.For<Loja>()
            .WithPredicate(x => x.Id == usuarioContexto!.Loja.Id)
            .FindFirstAsync(ct);

        if (loja == null)
            return Result.Fail(Erro.NaoEncontrado("Loja não encontrada"));

        var response = new GetLojaByContextResponse(
            loja.Id.EncodeWithSqids(),
            loja.NomeFantasia,
            loja.RazaoSocial,
            loja.Documento.Numero,
            loja.InscricaoEstadual,
            loja.InscricaoMunicipal,
            loja.Site,
            loja.LogoTipo,
            loja.Endereco != null ? new GetLojaByContextEnderecoResponse(
                loja.Endereco.Pais,
                loja.Endereco.Estado,
                loja.Endereco.Cidade,
                loja.Endereco.Logradouro,
                loja.Endereco.Bairro,
                loja.Endereco.Complemento,
                loja.Endereco.Cep.Valor,
                loja.Endereco.Numero
            ) : null!,
            loja.Contatos != null ? loja.Contatos.Select(contato => new GetLojaByContextContatoResponse(
                contato.Numero,
                contato.TipoTelefone.ToString()
            )).ToList() : new List<GetLojaByContextContatoResponse>(
        ));

        return Result.Success(response);
    }
}
