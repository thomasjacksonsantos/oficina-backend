

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.AreasProdutos.GetAreasProdutos;

public sealed record GetAreasProdutosRequest(
    int Pagina,
    int TotalPagina
) : AuthRequest;