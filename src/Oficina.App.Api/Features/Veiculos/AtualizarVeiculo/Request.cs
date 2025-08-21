
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Veiculos.AtualizarVeiculo;

public sealed record AtualizarVeiculoRequest(
    int Id,
    string Placa,
    string Modelo,
    int Hodrometro,
    string Cor,
    int Ano,
    string NumeroChassi,
    string NumeroSerie,
    string Motorizacao,
    string Chassi
) : AuthRequest;