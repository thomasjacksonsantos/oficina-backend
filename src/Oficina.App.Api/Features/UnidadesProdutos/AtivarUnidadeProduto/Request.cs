
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.UnidadesProdutos.AtivarUnidadeProduto;

public sealed record AtivarUnidadeProdutoRequest(
    [FromRoute] string Id
) : AuthRequest;