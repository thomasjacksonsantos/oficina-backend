using Oficina.Domain.Aggregates.UsuarioAggregates;
using Oficina.Domain.SeedWork;

namespace Oficina.Domain.ValueObjects;

public sealed record Contato
{
    public string DDD { get; set; }
    public string Numero { get; set; }
    public TipoTelefone TipoTelefone { get; set; }

#pragma warning disable CS8618
    public Contato() { }
#pragma warning restore CS8618

    private Contato(
        string ddd,
        string numero,
        TipoTelefone tipoTelefone)
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
            numero
        );

        if (result.IsFailed)
            return result;

        return new Contato(ddd, numero, TipoTelefone.Get(tipoTelefone)!);
    }

    private static Result Validar(
        string ddd,
        string numero)
    {
        var result = new Result();

        if (string.IsNullOrWhiteSpace(ddd))
            result.WithError(Erro.ValorNaoInformado(nameof(DDD)));

        if (string.IsNullOrWhiteSpace(numero))
            result.WithError(Erro.ValorNaoInformado(nameof(Numero)));

        return result;
    }
}