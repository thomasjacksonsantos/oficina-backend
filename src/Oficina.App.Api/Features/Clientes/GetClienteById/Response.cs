

namespace Oficina.App.Api.Features.Clientes.GetClienteById;

public sealed record GetClienteByIdResponse(
    string Nome,
    string Sexo,
    string TipoDocumento,
    string Documento,
    string EmailCliente,
    DateTime DataNascimento,
    ICollection<ContatoClienteResponse> Contatos,
    EnderecoClienteResponse Endereco
);

public sealed record ContatoClienteResponse(
    string Numero,
    string TipoTelefone
);

public sealed record EnderecoClienteResponse(
    string Pais,
    string Estado,
    string Cidade,
    string Logradouro,
    string Bairro,
    string Complemento,
    string Cep,
    string Numero
);