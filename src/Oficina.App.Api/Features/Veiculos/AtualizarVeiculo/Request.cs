
using Microsoft.AspNetCore.Mvc;
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Veiculos.AtualizarVeiculo;

public record AtualizarVeiculoRequest(
    [FromRoute] string Id,
    string Modelo,
    string Montadora,
    int Hodrometro,
    string Cor,
    int Ano,
    string NumeroSerie,
    string Motorizacao,
    string Chassi
) : AuthRequest;