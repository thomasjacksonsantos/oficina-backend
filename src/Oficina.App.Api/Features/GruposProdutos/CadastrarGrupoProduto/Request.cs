

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.GruposProdutos.CadastrarGrupoProduto;

public sealed record CadastrarGrupoProdutoRequest(
    string Descricao,
    string Area,
    string NCM,
    string ANP
) : AuthRequest;