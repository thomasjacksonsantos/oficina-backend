
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Veiculos.GetVeiculoById;

public sealed record GetVeiculoByIdRequest(
    [FromRoute] int Id
) : AuthRequest;