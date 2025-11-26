

using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Veiculos.CadastrarVeiculo;

public sealed record CadastrarVeiculoRequest(
    string Placa,
    string Modelo,
    string Montadora,
    int Hodrometro,
    string Cor,
    int Ano,
    string NumeroChassi,
    string NumeroSerie,
    string Motorizacao,
    string Chassi
): AuthRequest;