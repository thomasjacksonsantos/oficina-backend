

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.StatusPedidosCompras.GetStatusPedidosCompras;

public sealed record GetStatusPedidosComprasRequest(
    int Pagina,
    int TotalPagina
) : AuthRequest;