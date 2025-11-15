
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Clientes.CadastrarCliente;

public sealed record CadastrarClienteRequest(
    string Nome,
    string Sexo,
    string Documento,
    string EmailCliente,
    DateTime DataNascimento,
    ICollection<ContatoClienteRequest> Contatos,
    EnderecoClienteRequest Endereco
) : AuthRequest;

public sealed record ContatoClienteRequest(
    string Numero,
    string TipoTelefone
);

public sealed record EnderecoClienteRequest(
    string Pais,
    string Estado,
    string Cidade,
    string Logradouro,
    string Bairro,
    string Complemento,
    string Cep,
    string Numero
);