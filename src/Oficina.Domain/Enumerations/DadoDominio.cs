namespace Oficina.Domain.Enumerations;

public abstract record DadoDominio
{
    protected DadoDominio() { }

    protected DadoDominio(Guid id, string key)
        => (Id, Key) = (id, key);

    protected DadoDominio(Guid id, string key, string nome, string dominio)
        => (Id, Key, Nome, Dominio) = (id, key, nome, dominio);

    public Guid Id { get; private set; }
    public string Key { get; protected set; } = null!;
    public string Nome { get; protected set; } = null!;
    public string Dominio { get; protected set; } = null!;
}