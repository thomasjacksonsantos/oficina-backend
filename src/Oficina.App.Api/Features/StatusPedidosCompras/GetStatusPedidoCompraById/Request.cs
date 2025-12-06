

using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.StatusPedidosCompras.GetStatusPedidoCompraById;

public sealed record GetStatusPedidoCompraByIdRequest(
    [FromRoute] string Id
) : AuthRequest;