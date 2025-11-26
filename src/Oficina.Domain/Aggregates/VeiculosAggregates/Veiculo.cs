using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.VeiculoAggregates;

public class Veiculo
{
    public int Id { get; private set; }
    public string Placa { get; private set; }
    public string Modelo { get; private set; }
    public string Montadora { get; private set; }
    public int Hodrometro { get; private set; }
    public string Cor { get; private set; }
    public int Ano { get; private set; }
    public string NumeroSerie { get; private set; }
    public string Motorizacao { get; private set; }
    public string Chassi { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

#pragma warning disable CS8618 // Propriedade não inicializada
    private Veiculo() { }
#pragma warning restore CS8618

    public Veiculo(
        string placa,
        string modelo,
        string montadora,
        int hodrometro,
        string cor,
        int ano,
        string numeroChassi,
        string numeroSerie,
        string motorizacao,
        string chassi)
    {
        Placa = placa;
        Modelo = modelo;
        Montadora = montadora;
        Hodrometro = hodrometro;
        Cor = cor;
        Ano = ano;
        NumeroSerie = numeroSerie;
        Motorizacao = motorizacao;
        Chassi = chassi;
        Criado = DataHora.Criar().Value!;
        Atualizado = DataHora.Criar().Value!;
    }

    public Result<Veiculo> Atualizar(
        string placa,
        string modelo,
        string montadora,
        int hodrometro,
        string cor,
        int ano,
        string numeroChassi,
        string numeroSerie,
        string motorizacao,
        string chassi)
    {
        var result = new Result<Veiculo>();

        if (string.IsNullOrWhiteSpace(placa))
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Placa)}", "Valor informado para a Placa está inválido"));

        if (string.IsNullOrWhiteSpace(modelo))
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Modelo)}", "Valor informado para o Modelo está inválido"));

        if (string.IsNullOrWhiteSpace(montadora))
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Montadora)}", "Valor informado para a Montadora está inválido"));

        if (hodrometro < 0)
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Hodrometro)}", "Valor informado para o Hodômetro está inválido"));

        if (string.IsNullOrWhiteSpace(cor))
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Cor)}", "Valor informado para a Cor está inválido"));

        if (ano < 1900 || ano > DateTime.UtcNow.Year + 1)
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Ano)}", "Valor informado para o Ano está inválido"));

        if (string.IsNullOrWhiteSpace(numeroSerie))
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(NumeroSerie)}", "Valor informado para o Número de Série está inválido"));

        if (string.IsNullOrWhiteSpace(motorizacao))
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Motorizacao)}", "Valor informado para a Motorização está inválido"));

        if (string.IsNullOrWhiteSpace(chassi))
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Chassi)}", "Valor informado para o Chassi está inválido"));

        if (result.IsFailed)
            return result;

        Placa = placa;
        Modelo = modelo;
        Montadora = montadora;
        Hodrometro = hodrometro;
        Cor = cor;
        Ano = ano;
        NumeroSerie = numeroSerie;
        Motorizacao = motorizacao;
        Chassi = chassi;
        Atualizado = DataHora.Criar().Value!;

        return Result.Success(this);
    }

    public static Result<Veiculo> Criar(
        string placa,
        string modelo,
        string montadora,
        int hodrometro,
        string cor,
        int ano,
        string numeroChassi,
        string numeroSerie,
        string motorizacao,
        string chassi)
    {
        var result = new Result<Veiculo>();

         if (string.IsNullOrWhiteSpace(placa))
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Placa)}", "Valor informado para a Placa está inválido"));

        if (string.IsNullOrWhiteSpace(modelo))
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Modelo)}", "Valor informado para o Modelo está inválido"));

        if (string.IsNullOrWhiteSpace(montadora))
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Montadora)}", "Valor informado para a Montadora está inválido"));

        if (hodrometro < 0)
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Hodrometro)}", "Valor informado para o Hodômetro está inválido"));

        if (string.IsNullOrWhiteSpace(cor))
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Cor)}", "Valor informado para a Cor está inválido"));

        if (ano < 1900 || ano > DateTime.UtcNow.Year + 1)
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Ano)}", "Valor informado para o Ano está inválido"));

        if (string.IsNullOrWhiteSpace(numeroSerie))
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(NumeroSerie)}", "Valor informado para o Número de Série está inválido"));

        if (string.IsNullOrWhiteSpace(motorizacao))
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Motorizacao)}", "Valor informado para a Motorização está inválido"));

        if (string.IsNullOrWhiteSpace(chassi))
            result.WithError(Erro.ValorInvalido($"{nameof(Veiculo)}.{nameof(Chassi)}", "Valor informado para o Chassi está inválido"));

        var veiculo = new Veiculo(
            placa,
            modelo,
            montadora,
            hodrometro,
            cor,
            ano,
            numeroChassi,
            numeroSerie,
            motorizacao,
            chassi
        );

        return Result.Success(veiculo);
    }
}