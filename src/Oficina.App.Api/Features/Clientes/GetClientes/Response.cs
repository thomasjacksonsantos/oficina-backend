




using static Oficina.Domain.ValueObjects.Contato;

namespace Oficina.App.Api.Features.Clientes.GetClientes;

public sealed record GetClientesResponse(
    string Id,
    string Nome,
    string RazaoSocial,
    string Sexo,
    string TipoDocumento,
    string Documento,
    string EmailCliente,
    string ClienteStatus,
    DateTime DataNascimento,
    IEnumerable<ContatoClientesResponse> Contatos,
    EnderecoClientesResponse Endereco
);

public sealed record ContatoClientesResponse(
    string Numero,
    TipoTelefoneEnum TipoTelefone
);

public sealed record EnderecoClientesResponse(
    string Pais,
    string Estado,
    string Cidade,
    string Logradouro,
    string Bairro,
    string Complemento,
    string Cep,
    string Numero
);