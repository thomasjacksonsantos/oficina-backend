
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.AreasProdutos.AtivarAreaProduto;

public sealed record AtivarAreaProdutoRequest(
    [FromRoute] string Id
) : AuthRequest;