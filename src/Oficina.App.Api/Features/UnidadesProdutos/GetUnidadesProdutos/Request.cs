

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.UnidadesProdutos.GetUnidadesProdutos;

public sealed record GetUnidadesProdutosRequest(
    int Pagina,
    int TotalPagina
) : AuthRequest;