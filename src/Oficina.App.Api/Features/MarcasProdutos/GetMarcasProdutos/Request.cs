

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.MarcasProdutos.GetMarcasProdutos;

public sealed record GetMarcasProdutosRequest(
    int Pagina,
    int TotalPagina
) : AuthRequest;