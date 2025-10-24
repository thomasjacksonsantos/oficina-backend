namespace Oficina.Domain.SeedWork;

public abstract record ResultBase
{
    private readonly List<Erro> errors = new();
    
    protected ResultBase() { }
    protected ResultBase(List<Erro> errors) => this.errors = errors;
    
    public bool IsSuccess => errors.Count == 0;
    public bool IsFailed => !IsSuccess;
    
    public IList<Erro>? Errors => errors.AsReadOnly();
    
    public void WithError(params Erro[] parameters) => 
        errors.AddRange(parameters);
    
    public void WithErrors(IList<Erro> parameters) => 
        errors.AddRange(parameters);
}

public sealed record Result : ResultBase
{
    public Result() { }
    private Result(List<Erro> errors) : base(errors) { }
    
    public static Result Success() => new();
    public static Result<T> Success<T>(T value) => Result<T>.Success(value);
    public static Result Fail(string message) => new(Erro.Error(string.Empty, message));
    public static Result Fail(IList<Erro> error) => new(error.ToList());
    
    public static implicit operator Result(Erro error) => Fail([error]);
}

public sealed record Result<T> : ResultBase
{
    public Result() { }
    private Result(T value) => Value = value;
    private Result(List<Erro> error) : base(error) { }
    
    public T? Value { get; }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Fail(IList<Erro> error) => new(error.ToList());
    public static Result<T> Fail<T2>(Result<T2> result) => new(result.Errors!.ToList());
    

    public static implicit operator Result<T>(Erro error) => Fail([error]);
    
    public static implicit operator Result<T>(Result result)
    {
        if (result.IsSuccess)
            throw new NotSupportedException("Conversão inválida");

        return new Result<T>(result.Errors!.ToList());
    }
    
    public static implicit operator Result<T>(T value) => new(value);
    
}