
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Lojas.AtualizarLoja;

public record AtualizarLojaRequest(
    [FromRoute] string Id,
    string NomeFantasia,
    string RazaoSocial,
    string Documento,
    string InscricaoEstadual,
    string InscricaoMunicipal,
    string Site,
    string LogoTipo,
    AtualizarLojaEndereco Endereco,
    ICollection<AtualizarLojaContato> Contatos
) : AuthRequest;

public record AtualizarLojaContato(
    string DDD,
    string Numero,
    string TipoTelefone
);

public record AtualizarLojaEndereco(
    string Pais,
    string Estado,
    string Cidade,
    string Logradouro,
    string Bairro,
    string Complemento,
    string Cep,
    string Numero
);
