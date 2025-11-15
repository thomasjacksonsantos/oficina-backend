
using System.Text.RegularExpressions;
using Oficina.Domain.SeedWork;

namespace Oficina.Domain.ValueObjects;

public sealed record Contato
{
    public enum TipoTelefoneEnum
    {
        Telefone,
        Celular
    }
    public string Numero { get; set; }
    public TipoTelefoneEnum TipoTelefone { get; set; }

#pragma warning disable CS8618
    public Contato() { }
#pragma warning restore CS8618

    public Contato(     
        string numero,
        TipoTelefoneEnum tipoTelefone)
    {
        Numero = numero;
        TipoTelefone = tipoTelefone;
    }

    public static Result<Contato> Criar(
        string numero,
        string tipoTelefone)
    {
        numero = string.Join("", new Regex(@"\d+").Matches(numero));
        var result = Validar(
            numero,
            tipoTelefone
        );

        if (result.IsFailed)
            return result;

        Enum.TryParse<TipoTelefoneEnum>(tipoTelefone, true, out var tipoTelefoneValue);

        return new Contato(numero, tipoTelefoneValue);
    }

    private static Result Validar(
        string numero,
        string tipoTelefone)
    {
        var result = new Result();

        if (string.IsNullOrWhiteSpace(numero))
            result.WithError(Erro.ValorInvalido($"{nameof(Contato)}.{nameof(Numero)}", "Valor informado é inválido."));

        if (!Enum.TryParse<TipoTelefoneEnum>(tipoTelefone, true, out var tipoTelefoneValue))
            result.WithError(Erro.ValorInvalido($"{nameof(Contato)}.{nameof(TipoTelefone)}", "Valor informado é inválido."));

        return result;
    }
}