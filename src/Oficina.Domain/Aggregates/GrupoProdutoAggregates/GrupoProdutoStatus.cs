using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.GrupoProdutoAggregates;

public record GrupoProdutoStatus : DadoDominio
{
private GrupoProdutoStatus() : base() { }

    private GrupoProdutoStatus(Guid id, string key, string nome, string dominio) : base(id, key, nome, dominio) { }

    public static GrupoProdutoStatus Get(string key) =>
        key.ToLower() switch
        {
            "ativo" => Ativo,
            "inativo" => Inativo,
            _ => throw new ArgumentOutOfRangeException($"Grupo Produto Status inválido: {key}")
        };

    public static GrupoProdutoStatus Get(Guid id) =>
        id == Ativo.Id ? Ativo :
        id == Inativo.Id ? Inativo :
        throw new ArgumentOutOfRangeException($"Grupo Produto Status inválido: {id}");

    public static readonly GrupoProdutoStatus Ativo = ("0865836b-7a45-4f85-9c7e-5a7ac6e65e55", "Ativo", "Ativo", "GrupoProdutoStatus");
    public static readonly GrupoProdutoStatus Inativo = ("9f8b5e8c-6e7d-4ea4-9f2d-3be51f6b9cd2", "Inativo", "Inativo", "GrupoProdutoStatus");


    public static implicit operator GrupoProdutoStatus((string Id, string Key, string Nome, string Dominio) data) =>
        new(new Guid(data.Id), data.Key, data.Nome, data.Dominio);
}
