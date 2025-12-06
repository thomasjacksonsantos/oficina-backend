

using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.GruposProdutos.AtualizarGrupoProduto;

public sealed record AtualizarGrupoProdutoRequest(
    [FromRoute] string Id,
    string Descricao,
    string Area,
    string NCM,
    string ANP
) : AuthRequest;