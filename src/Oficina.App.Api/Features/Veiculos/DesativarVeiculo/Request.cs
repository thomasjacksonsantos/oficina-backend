
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Veiculos.DesativarVeiculo;

public sealed record DesativarVeiculoRequest(
    [FromRoute] string Id
) : AuthRequest;