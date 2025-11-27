

namespace Oficina.App.Api.Features.Lojas.GetLojas;

public record GetLojasResponse(
    string Id,
    string NomeFantasia,
    string RazaoSocial,
    string Documento,
    string InscricaoEstadual,
    string InscricaoMunicipal,
    string Site,
    string LogoTipo,
    GetLojasEnderecoResponse Endereco,
    ICollection<GetLojasContatoResponse> Contatos
);

public record GetLojasContatoResponse(
    string Numero,
    string TipoTelefone
);

public record GetLojasEnderecoResponse(
    string Pais,
    string Estado,
    string Cidade,
    string Logradouro,
    string Bairro,
    string Complemento,
    string Cep,
    string Numero
);
