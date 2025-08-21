

namespace Oficina.App.Api.Features.Veiculos.GetVeiculoById;

public sealed record GetVeiculoByIdResponse(
    string Placa,
    string Modelo,
    int Hodrometro,
    string Cor,
    int Ano,
    string NumeroChassi,
    string NumeroSerie,
    string Motorizacao,
    string Chassi
);