
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.UnidadesProdutos.DesativarUnidadeProduto;

public sealed record DesativarUnidadeProdutoRequest(
    [FromRoute] string Id
) : AuthRequest;