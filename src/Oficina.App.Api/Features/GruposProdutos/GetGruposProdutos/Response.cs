

namespace Oficina.App.Api.Features.GruposProdutos.GetGruposProdutos;

public sealed record GetGruposProdutosResponse(
    string Id,
    string Descricao,
    string Area,
    string NCM,
    string ANP,
    string GrupoProdutoStatus,
    DateTime Criado,
    DateTime Atualizado
);