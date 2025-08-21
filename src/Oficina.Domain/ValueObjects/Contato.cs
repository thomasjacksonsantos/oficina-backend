
using Oficina.Domain.SeedWork;

namespace Oficina.Domain.ValueObjects;

public sealed record Contato
{
    public enum TipoTelefoneEnum
    {
        Telefone,
        Celular
    }

    public string DDD { get; set; }
    public string Numero { get; set; }
    public TipoTelefoneEnum TipoTelefone { get; set; }

#pragma warning disable CS8618
    public Contato() { }
#pragma warning restore CS8618

    public Contato(
        string ddd,
        string numero,
        TipoTelefoneEnum tipoTelefone)
    {
        DDD = ddd;
        Numero = numero;
        TipoTelefone = tipoTelefone;
    }

    public static Result<Contato> Criar(
        string ddd,
        string numero,
        string tipoTelefone)
    {
        var result = Validar(
            ddd,
            numero,
            tipoTelefone
        );

        if (result.IsFailed)
            return result;

        Enum.TryParse<TipoTelefoneEnum>(tipoTelefone, true, out var tipoTelefoneValue);

        return new Contato(ddd, numero, tipoTelefoneValue);
    }

    private static Result Validar(
        string ddd,
        string numero,
        string tipoTelefone)
    {
        var result = new Result();

        if (string.IsNullOrWhiteSpace(ddd))
            result.WithError(Erro.ValorNaoInformado(nameof(DDD)));

        if (string.IsNullOrWhiteSpace(numero))
            result.WithError(Erro.ValorNaoInformado(nameof(Numero)));

        if (!Enum.TryParse<TipoTelefoneEnum>(tipoTelefone, true, out var tipoTelefoneValue))
            result.WithError(Erro.Error("", $"TipoTelefone nao foi encontrado {tipoTelefone}"));

        return result;
    }
}