
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.OrdemDeServico.GetAllOrdemDeServico;

public sealed record GetAllOrdemDeServicoRequest(
    string Documento,
    int Pagina,
    int TotalPagina
) : AuthRequest;