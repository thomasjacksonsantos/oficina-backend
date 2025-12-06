

namespace Oficina.App.Api.Features.StatusPedidosCompras.GetStatusPedidoCompraById;

public sealed record GetStatusPedidoCompraByIdResponse(
    string Id,
    string Descricao,
    string StatusPedidoCompra,
    DateTime Criado,
    DateTime Atualizado
);