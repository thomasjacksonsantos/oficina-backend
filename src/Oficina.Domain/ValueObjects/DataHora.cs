using Oficina.Domain.SeedWork;

namespace Oficina.Domain.ValueObjects;

public sealed class DataHora
{
    public DateTime Valor { get; private set; }

    private DataHora(DateTime valor)
    {
        Valor = valor;
    }

    public static Result<DataHora> Criar(DateTime valor)
    {
        var result = Validar(valor);

        if (result.IsFailed)
            return result;

        return new DataHora(valor);
    }

    public static Result<DataHora> Criar()
        => Result.Success(new DataHora(DateTime.Now));
    
    public static implicit operator DataHora(DateTime datetime)
    {
        var result = Criar(datetime);
        
        if (result.IsSuccess is false)
            throw new ArgumentException(result.Errors!.First().Descricao);
        
        return result.Value!;
    }

    private static Result Validar(
        DateTime dataNascimento)
    {
        var result = new Result();

        if (dataNascimento <= DateTime.MinValue || dataNascimento >= DateTime.MaxValue)
            result.WithError(Erro.ValorNaoInformado(nameof(DataHora)));

        return result;
    }
}