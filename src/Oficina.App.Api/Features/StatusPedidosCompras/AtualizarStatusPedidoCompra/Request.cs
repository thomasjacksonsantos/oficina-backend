

using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.StatusPedidosCompras.AtualizarStatusPedidoCompra;

public sealed record AtualizarStatusPedidoCompraRequest(
    [FromRoute] string Id,
    string Descricao
) : AuthRequest;