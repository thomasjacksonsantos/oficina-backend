

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Veiculos.GetVeiculos;

public sealed record GetVeiculosRequest(
    int Pagina,
    int TotalPagina
) : AuthRequest;