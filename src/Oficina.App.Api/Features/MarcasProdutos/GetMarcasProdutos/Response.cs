

namespace Oficina.App.Api.Features.MarcasProdutos.GetMarcasProdutos;

public sealed record GetMarcasProdutosResponse(
    string Id,
    string Descricao,    
    string MarcaProdutoStatus,
    DateTime Criado,
    DateTime Atualizado
);