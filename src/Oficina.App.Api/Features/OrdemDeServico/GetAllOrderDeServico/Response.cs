

namespace Oficina.App.Api.Features.OrdemDeServico.GetAllOrdemDeServico;


public sealed record GetAllOrdemDeServicoResponse(
    int Id,
    DateTime DataInicio,
    DateTime DataFim,
    DateTime? DataPrevisao,
    string Veiculo,
    string Placa,
    string Cliente,
    string Documento,
    string Responsavel,
    string Status
);