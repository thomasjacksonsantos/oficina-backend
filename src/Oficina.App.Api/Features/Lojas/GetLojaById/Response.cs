

namespace Oficina.App.Api.Features.Lojas.GetLojaById;

public record GetLojaByIdResponse(
    string Id,
    string NomeFantasia,
    string RazaoSocial,
    string Documento,
    string InscricaoEstadual,
    string InscricaoMunicipal,
    string Site,
    string LogoTipo,
    GetLojaByIdEnderecoResponse Endereco,
    ICollection<GetLojaByIdContatoResponse> Contatos
);

public record GetLojaByIdContatoResponse(
    string Numero,
    string TipoTelefone
);

public record GetLojaByIdEnderecoResponse(
    string Pais,
    string Estado,
    string Cidade,
    string Logradouro,
    string Bairro,
    string Complemento,
    string Cep,
    string Numero
);
