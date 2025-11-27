
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Veiculos.AtivarVeiculo;

public sealed record AtivarVeiculoRequest(
    [FromRoute] string Id
) : AuthRequest;