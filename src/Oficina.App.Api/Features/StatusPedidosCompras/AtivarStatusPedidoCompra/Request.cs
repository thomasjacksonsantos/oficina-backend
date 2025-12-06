
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.StatusPedidosCompras.AtivarStatusPedidoCompra;

public sealed record AtivarStatusPedidoCompraRequest(
    [FromRoute] string Id
) : AuthRequest;