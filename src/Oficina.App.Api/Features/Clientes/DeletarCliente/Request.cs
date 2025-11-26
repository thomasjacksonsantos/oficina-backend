
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Clientes.DeletarCliente;

public sealed record DeletarClienteRequest(
    [FromRoute] string Id
) : AuthRequest;