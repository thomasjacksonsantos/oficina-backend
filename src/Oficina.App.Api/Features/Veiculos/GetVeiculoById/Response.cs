

namespace Oficina.App.Api.Features.Veiculos.GetVeiculoById;

public sealed record GetVeiculoByIdResponse(
    string Placa,
    string Modelo,
    string Montadora,
    int Hodrometro,
    string Cor,
    int Ano,
    string NumeroSerie,
    string Motorizacao,
    string Chassi
);