

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Contas.GetContas;

public record GetContasRequest(
    string Id
) : AuthRequest;