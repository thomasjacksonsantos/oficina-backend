

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.StatusPedidosCompras.CadastrarStatusPedidoCompra;

public sealed record CadastrarStatusPedidoCompraRequest(
    string Descricao
) : AuthRequest;