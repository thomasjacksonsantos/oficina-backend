using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.VeiculoAggregates;

public class Veiculo
{
    public int Id { get; private set; }
    public string Placa { get; private set; }
    public string Modelo { get; private set; }
    public int Hodrometro { get; private set; }
    public string Cor { get; private set; }
    public int Ano { get; private set; }
    public string NumeroChassi { get; private set; }
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
        Hodrometro = hodrometro;
        Cor = cor;
        Ano = ano;
        NumeroChassi = numeroChassi;
        NumeroSerie = numeroSerie;
        Motorizacao = motorizacao;
        Chassi = chassi;
        Criado = DataHora.Criar().Value!;
        Atualizado = DataHora.Criar().Value!;
    }

    public static Result<Veiculo> Criar(
        string placa,
        string modelo,
        int hodrometro,
        string cor,
        int ano,
        string numeroChassi,
        string numeroSerie,
        string motorizacao,
        string chassi)
    {
        var erros = new List<string>();

        if (string.IsNullOrWhiteSpace(placa))
            erros.Add("Placa não pode ser vazia.");

        if (string.IsNullOrWhiteSpace(modelo))
            erros.Add("Modelo não pode ser vazio.");

        if (hodrometro < 0)
            erros.Add("Hodômetro não pode ser negativo.");

        if (string.IsNullOrWhiteSpace(cor))
            erros.Add("Cor não pode ser vazia.");

        if (ano < 1900 || ano > DateTime.UtcNow.Year + 1)
            erros.Add("Ano do veículo inválido.");

        if (string.IsNullOrWhiteSpace(numeroChassi))
            erros.Add("Número do chassi não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(numeroSerie))
            erros.Add("Número de série não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(motorizacao))
            erros.Add("Motorização não pode ser vazia.");

        if (string.IsNullOrWhiteSpace(chassi))
            erros.Add("Chassi não pode ser vazio.");

        if (erros.Any())
            return Result.Fail(string.Join(" ", erros));

        var veiculo = new Veiculo(
            placa,
            modelo,
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