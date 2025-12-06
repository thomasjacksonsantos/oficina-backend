

namespace Oficina.App.Api.Features.GruposProdutos.GetGrupoProdutoById;

public sealed record GetGrupoProdutoByIdResponse(
    string Id,
    string Descricao,
    string Area,
    string NCM,
    string ANP,
    string GrupoProdutoStatus,
    DateTime Criado,
    DateTime Atualizado
);