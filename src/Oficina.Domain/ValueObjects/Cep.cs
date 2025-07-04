using System.Text.RegularExpressions;
using Oficina.Domain.SeedWork;

namespace Oficina.Domain.ValueObjects;

public record Cep
{
    public string Valor { get; set; }
#pragma warning disable CS8618
    public Cep() { }
#pragma warning restore CS8618
    private Cep(string valor)
    {
        Valor = valor;
    }

    public static Result<Cep> Criar(string valor)
    {
        var result = Validar(
            valor
        );

        if (result.IsFailed)
            return result;

        return new Cep(valor);
    }

    private static Result Validar(
        string valor
    )
    {
        var result = new Result();

        if (string.IsNullOrWhiteSpace(valor))
            result.WithError(Erro.ValorNaoInformado(nameof(Cep)));

        var onlyNumbers = string.Join("", new Regex(@"\d+").Matches(valor));

        if (onlyNumbers.Length != 8)
            result.WithError(CepErros.QuantidadeCaracteresInvalido);

        return result;
    }

    private static class CepErros
    {
        public static Erro QuantidadeCaracteresInvalido =>
            Erro.Validacao(
                "Cep.QuantidadeCaracteresInvalido",
                "O cep informado deve conter 6 caracteres");
    }
}