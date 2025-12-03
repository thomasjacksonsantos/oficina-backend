

namespace Oficina.App.Api.Features.Lojas.GetLojaByContext;

public record GetLojaByContextResponse(
    string Id,
    string NomeFantasia,
    string RazaoSocial,
    string Documento,
    string InscricaoEstadual,
    string InscricaoMunicipal,
    string Site,
    string LogoTipo,
    GetLojaByContextEnderecoResponse Endereco,
    ICollection<GetLojaByContextContatoResponse> Contatos
);

public record GetLojaByContextContatoResponse(
    string Numero,
    string TipoTelefone
);

public record GetLojaByContextEnderecoResponse(
    string Pais,
    string Estado,
    string Cidade,
    string Logradouro,
    string Bairro,
    string Complemento,
    string Cep,
    string Numero
);
