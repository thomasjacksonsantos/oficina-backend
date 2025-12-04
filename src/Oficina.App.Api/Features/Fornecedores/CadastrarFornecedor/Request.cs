

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Fornecedores.CadastrarFornecedor;

public sealed record CadastrarFornecedorRequest(
    string NomeFantasia,
    string Site,
    string InscricaoEstadual,
    string InscricaoMunicipal,
    string TipoConsumidor,
    string Documento,
    string EmailFornecedor,
    DateTime DataNascimento,
    CadastrarFornecedorEndereco Endereco,
    ICollection<CadastrarFornecedorContato> Contatos
) : AuthRequest;

public record CadastrarFornecedorContato(
    string Numero,
    string TipoTelefone
);

public record CadastrarFornecedorEndereco(
    string Pais,
    string Estado,
    string Cidade,
    string Logradouro,
    string Bairro,
    string Complemento,
    string Cep,
    string Numero
);