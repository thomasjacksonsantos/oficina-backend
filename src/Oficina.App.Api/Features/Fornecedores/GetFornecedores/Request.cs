

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Fornecedores.GetFornecedores;

public sealed record GetFornecedoresRequest(
    int Pagina,
    int TotalPagina
) : AuthRequest;