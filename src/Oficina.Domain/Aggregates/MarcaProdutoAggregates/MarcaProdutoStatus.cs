using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.MarcaProdutoAggregates;

public record MarcaProdutoStatus : DadoDominio
{
private MarcaProdutoStatus() : base() { }
    private MarcaProdutoStatus(Guid id, string key, string nome, string dominio) : base(id, key, nome, dominio) { }

    public static MarcaProdutoStatus Get(string key) =>
        key.ToLower() switch
        {
            "ativo" => Ativo,
            "inativo" => Inativo,
            _ => throw new ArgumentOutOfRangeException($"Marca Produto Status inválido: {key}")
        };

    public static MarcaProdutoStatus Get(Guid id) =>
        id == Ativo.Id ? Ativo :
        id == Inativo.Id ? Inativo :
        throw new ArgumentOutOfRangeException($"Marca Produto Status inválido: {id}");

    public static readonly MarcaProdutoStatus Ativo = ("4e7b2c1a-9d3f-4b6a-8c2e-5f1a7b3c6d8e", "Ativo", "Ativo", "MarcaProdutoStatus");
    public static readonly MarcaProdutoStatus Inativo = ("8a1c5e2b-3d7f-4c9a-9b2e-6d4f7a1b2c3e", "Inativo", "Inativo", "MarcaProdutoStatus");

    public static implicit operator MarcaProdutoStatus((string Id, string Key, string Nome, string Dominio) data) =>
        new(new Guid(data.Id), data.Key, data.Nome, data.Dominio);
}
