using Oficina.Domain.SeedWork;

namespace Oficina.Domain.ValueObjects;

public sealed class DataNascimento
{
    public DateTime Valor { get; private set; }

    private DataNascimento(DateTime valor)
    {
        Valor = valor;
    }

    public static Result<DataNascimento> Criar(DateTime valor)
    {
        var result = Validar(valor);

        if (result.IsFailed)
            return result;

        return new DataNascimento(valor);
    }
    
    public static implicit operator DataNascimento(DateTime datetime)
    {
        var result = Criar(datetime);
        
        if (result.IsSuccess is false)
            throw new ArgumentException("Data inválida.");
        
        return result.Value!;
    }

    private static Result Validar(
        DateTime dataNascimento)
    {
        var result = new Result();

        if (dataNascimento <= DateTime.MinValue || dataNascimento >= DateTime.MaxValue)
            result.WithError(Erro.ValorNaoInformado(nameof(DataNascimento)));

        return result;
    }
}