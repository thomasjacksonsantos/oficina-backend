

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.GruposProdutos.GetGruposProdutos;

public sealed record GetGruposProdutosRequest(
    int Pagina,
    int TotalPagina
) : AuthRequest;