

namespace Oficina.App.Api.Features.Fornecedores.GetFornecedores;

public sealed record GetFornecedoresResponse(
    string Id,
    string NomeFantasia,
    string Documento,
    IEnumerable<GetFornecedoresContato> Contatos,
    string FornecedorStatus  
);

public record GetFornecedoresContato(
    string Numero,
    string TipoTelefone
);