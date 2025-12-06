

namespace Oficina.App.Api.Features.StatusPedidosCompras.GetStatusPedidosCompras;

public sealed record GetStatusPedidosComprasResponse(
    string Id,
    string Descricao,    
    string StatusPedidoCompraStatus,
    DateTime Criado,
    DateTime Atualizado
);