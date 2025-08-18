

namespace Oficina.App.Api.Features.Clientes.GetClientes;

public sealed record GetClientesResponse(
    int? Id,
    string? Nome,
    string? Documento
);