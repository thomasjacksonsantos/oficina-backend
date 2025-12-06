
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.StatusPedidosCompras.DesativarStatusPedidoCompra;

public sealed record DesativarStatusPedidoCompraRequest(
    [FromRoute] string Id
) : AuthRequest;