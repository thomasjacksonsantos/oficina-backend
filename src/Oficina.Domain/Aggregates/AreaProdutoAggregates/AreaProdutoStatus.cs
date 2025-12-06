using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.AreaProdutoAggregates;

public record AreaProdutoStatus : DadoDominio
{
private AreaProdutoStatus() : base() { }
    private AreaProdutoStatus(Guid id, string key, string nome, string dominio) : base(id, key, nome, dominio) { }

    public static AreaProdutoStatus Get(string key) =>
        key.ToLower() switch
        {
            "ativo" => Ativo,
            "inativo" => Inativo,
            _ => throw new ArgumentOutOfRangeException($"Area Produto Status inválido: {key}")
        };

    public static AreaProdutoStatus Get(Guid id) =>
        id == Ativo.Id ? Ativo :
        id == Inativo.Id ? Inativo :
        throw new ArgumentOutOfRangeException($"Area Produto Status inválido: {id}");

    public static readonly AreaProdutoStatus Ativo = ("7e2b1c3a-4f6d-4e2a-9b1a-2c3d4e5f6a7b", "Ativo", "Ativo", "AreaProdutoStatus");
    public static readonly AreaProdutoStatus Inativo = ("1a9c8b7d-2e3f-4c5a-8b6d-7e1f2a3c4b5d", "Inativo", "Inativo", "AreaProdutoStatus");


    public static implicit operator AreaProdutoStatus((string Id, string Key, string Nome, string Dominio) data) =>
        new(new Guid(data.Id), data.Key, data.Nome, data.Dominio);
}
