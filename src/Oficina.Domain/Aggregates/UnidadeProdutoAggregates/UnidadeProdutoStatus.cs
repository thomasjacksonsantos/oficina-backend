using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.UnidadeProdutoAggregates;

public record UnidadeProdutoStatus : DadoDominio
{
private UnidadeProdutoStatus() : base() { }
    private UnidadeProdutoStatus(Guid id, string key, string nome, string dominio) : base(id, key, nome, dominio) { }

    public static UnidadeProdutoStatus Get(string key) =>
        key.ToLower() switch
        {
            "ativo" => Ativo,
            "inativo" => Inativo,
            _ => throw new ArgumentOutOfRangeException($"Unidade Produto Status inválido: {key}")
        };

    public static UnidadeProdutoStatus Get(Guid id) =>
        id == Ativo.Id ? Ativo :
        id == Inativo.Id ? Inativo :
        throw new ArgumentOutOfRangeException($"Unidade Produto Status inválido: {id}");

    public static readonly UnidadeProdutoStatus Ativo = ("3c7e2a1b-8f4d-4c2a-9b7e-5d6a8c1f2b3a", "Ativo", "Ativo", "UnidadeProdutoStatus");
    public static readonly UnidadeProdutoStatus Inativo = ("9a2b6c4d-1e7f-4b3a-8c2d-3f5e7a9b1c6d", "Inativo", "Inativo", "UnidadeProdutoStatus");


    public static implicit operator UnidadeProdutoStatus((string Id, string Key, string Nome, string Dominio) data) =>
        new(new Guid(data.Id), data.Key, data.Nome, data.Dominio);
}
