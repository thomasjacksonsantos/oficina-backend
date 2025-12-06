

namespace Oficina.App.Api.Features.AreasProdutos.GetAreaProdutoById;

public sealed record GetAreaProdutoByIdResponse(
    string Id,
    string Descricao,
    string AreaProdutoStatus,
    DateTime Criado,
    DateTime Atualizado
);