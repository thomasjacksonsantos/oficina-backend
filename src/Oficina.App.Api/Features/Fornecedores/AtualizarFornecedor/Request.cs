
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Fornecedores.AtualizarFornecedor;

public sealed record AtualizarFornecedorRequest(
    [FromRoute] string Id,
    string NomeFantasia,
    string Site,
    string InscricaoEstadual,
    string InscricaoMunicipal,
    string TipoConsumidor,
    string Documento,
    string EmailFornecedor,
    DateTime DataNascimento,
    AtualizarFornecedorEndereco Endereco,
    ICollection<AtualizarFornecedorContato> Contatos
) : AuthRequest;

public record AtualizarFornecedorContato(
    string Numero,
    string TipoTelefone
);

public record AtualizarFornecedorEndereco(
    string Pais,
    string Estado,
    string Cidade,
    string Logradouro,
    string Bairro,
    string Complemento,
    string Cep,
    string Numero
);