
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Fornecedores.AtivarFornecedor;

public sealed record AtivarFornecedorRequest(
    [FromRoute] string Id
) : AuthRequest;