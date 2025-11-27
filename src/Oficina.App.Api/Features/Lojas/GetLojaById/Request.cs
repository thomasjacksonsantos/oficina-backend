
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Lojas.GetLojaById;

public record GetLojaByIdRequest(
    [FromRoute] string Id
) : AuthRequest;