
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.OrdemDeServico.GetOrdemDeServicoById;

public sealed record GetOrdemDeServicoByIdRequest(
    int Id
) : AuthRequest;