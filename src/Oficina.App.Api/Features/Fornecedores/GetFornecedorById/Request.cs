
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Fornecedores.GetFornecedorById;

public sealed record GetFornecedorByIdRequest(
    [FromRoute] string Id
) : AuthRequest;