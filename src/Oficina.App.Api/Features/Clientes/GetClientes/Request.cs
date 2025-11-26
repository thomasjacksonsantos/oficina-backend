
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Clientes.GetClientes;

public sealed record GetClientesRequest(
    [FromQuery] string Documento,
    [FromQuery] string Nome,
    [FromQuery] int Pagina,
    [FromQuery] int TotalPagina
) : AuthRequest;