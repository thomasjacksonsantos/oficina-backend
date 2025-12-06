

namespace Oficina.App.Api.Features.MarcasProdutos.GetMarcaProdutoById;

public sealed record GetMarcaProdutoByIdResponse(
    string Id,
    string Descricao,
    string MarcaProdutoStatus,
    DateTime Criado,
    DateTime Atualizado
);