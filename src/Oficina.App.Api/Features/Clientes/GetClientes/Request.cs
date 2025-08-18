
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Clientes.GetClientes;

public sealed record GetClientesRequest(
    string Documento,
    int Pagina,
    int TotalPagina
) : AuthRequest;