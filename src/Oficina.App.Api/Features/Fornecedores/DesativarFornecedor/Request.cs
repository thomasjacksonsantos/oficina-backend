
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Fornecedores.DesativarFornecedor;

public sealed record DesativarFornecedorRequest(
    [FromRoute] string Id
) : AuthRequest;