

namespace Oficina.App.Api.Features.Veiculos.GetVeiculos;

public sealed record GetVeiculosResponse(
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