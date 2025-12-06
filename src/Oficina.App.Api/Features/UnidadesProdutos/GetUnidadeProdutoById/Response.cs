

namespace Oficina.App.Api.Features.UnidadesProdutos.GetUnidadeProdutoById;

public sealed record GetUnidadeProdutoByIdResponse(
    string Id,
    string Descricao,
    string UnidadeProdutoStatus,
    DateTime Criado,
    DateTime Atualizado
);