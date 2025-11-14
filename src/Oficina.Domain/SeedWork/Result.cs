namespace Oficina.Domain.SeedWork;

public abstract record ResultBase
{
    protected ResultBase() { }
    protected ResultBase(List<Erro> errors) => Errors = errors;
    
    public bool IsSuccess => Errors.Count == 0;
    public bool IsFailed => !IsSuccess;

    public List<Erro> Errors { get; private set; } = [];
    
    public void WithError(params Erro[] parameters) => 
        Errors.AddRange(parameters);
    
    public void WithErrors(IList<Erro> parameters) => 
        Errors.AddRange(parameters);
}

public sealed record Result : ResultBase
{
    public Result() { }
    private Result(List<Erro> errors) : base(errors) { }
    
    public static Result Success() => new();
    public static Result<T> Success<T>(T value) => Result<T>.Success(value);
    public static Result Fail(IList<Erro> error) => new(error.ToList());
    public static Result Fail(Erro erro) => new([erro]);

    public static implicit operator Result(Erro error) => Fail([error]);
    public static implicit operator Result(List<Erro> errors) => Fail(errors);
}

public sealed record Result<T> : ResultBase
{
    public Result() { }
    private Result(T value) => Value = value;
    private Result(List<Erro> error) : base(error) { }
    
    public T? Value { get; }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Fail(IList<Erro> error) => new(error.ToList());

    public static implicit operator Result<T>(Erro error) => Fail([error]);
    
    public static implicit operator Result<T>(List<Erro> errors) => Fail(errors);

    public static implicit operator Result<T>(Result result)
    {
        if (result.IsSuccess)
            throw new NotSupportedException("Conversão inválida");

        return new Result<T>(result.Errors!.ToList());
    }
    
    public static implicit operator Result<T>(T value) => new(value);    
}