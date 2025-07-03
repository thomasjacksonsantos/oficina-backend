using Oficina.Domain.SeedWork;

namespace Oficina.Domain.ValueObjects;

public sealed class Carteirinha
{
    public string Valor { get; private set; }

    private Carteirinha(string valor)
    {
        Valor = valor.ToLower();
    }

    public static Result<Carteirinha> Criar(string valor)
    {
        var result = Validar(valor);

        if (result.IsFailed)
            return result;

        return new Carteirinha(valor);
    }

    private static Result Validar(
        string nome
    )
    {
        var result = new Result();

        if (string.IsNullOrWhiteSpace(nome))
            result.WithError(
                Erro.ValorNaoInformado(nameof(Carteirinha)));

        return result;
    }
}