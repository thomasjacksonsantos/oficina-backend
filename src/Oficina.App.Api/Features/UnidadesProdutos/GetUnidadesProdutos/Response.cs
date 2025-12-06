

namespace Oficina.App.Api.Features.UnidadesProdutos.GetUnidadesProdutos;

public sealed record GetUnidadesProdutosResponse(
    string Id,
    string Descricao,    
    string UnidadeProdutoStatus,
    DateTime Criado,
    DateTime Atualizado
);