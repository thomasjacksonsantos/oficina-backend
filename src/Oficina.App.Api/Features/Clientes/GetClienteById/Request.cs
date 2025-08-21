
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Clientes.GetClienteById;

public sealed record GetClienteByIdRequest(
    [FromRoute] int Id
) : AuthRequest;