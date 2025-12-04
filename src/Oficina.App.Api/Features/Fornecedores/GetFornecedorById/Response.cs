

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Fornecedores.GetFornecedorById;

public sealed record GetFornecedorByIdResponse(
    string Id,
    string NomeFantasia,
    string Site,
    string InscricaoEstadual,
    string InscricaoMunicipal,
    string TipoConsumidor,
    string Documento,
    string EmailFornecedor,
    string FornecedorStatus,
    DateTime DataNascimento,
    GetFornecedorEndereco Endereco,
    ICollection<GetFornecedorContato> Contatos
) : AuthRequest;

public record GetFornecedorContato(
    string Numero,
    string TipoTelefone
);

public record GetFornecedorEndereco(
    string Pais,
    string Estado,
    string Cidade,
    string Logradouro,
    string Bairro,
    string Complemento,
    string Cep,
    string Numero
);