
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.AreasProdutos.DesativarAreaProduto;

public sealed record DesativarAreaProdutoRequest(
    [FromRoute] string Id
) : AuthRequest;