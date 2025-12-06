

using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.AreasProdutos.AtualizarAreaProduto;

public sealed record AtualizarAreaProdutoRequest(
    [FromRoute] string Id,
    string Descricao,
    string Garantia
) : AuthRequest;