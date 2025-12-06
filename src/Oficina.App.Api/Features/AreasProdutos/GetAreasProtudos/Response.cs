

namespace Oficina.App.Api.Features.AreasProdutos.GetAreasProdutos;

public sealed record GetAreasProdutosResponse(
    string Id,
    string Descricao,
    string Garantia,
    string AreaProdutoStatus,
    DateTime Criado,
    DateTime Atualizado
);