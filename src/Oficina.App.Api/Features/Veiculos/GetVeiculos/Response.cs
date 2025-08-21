

namespace Oficina.App.Api.Features.Veiculos.GetVeiculos;

public sealed record GetVeiculosResponse(
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