using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.ClienteAggregates;

public record ClienteStatus : DadoDominio
{
private ClienteStatus() : base() { }

    private ClienteStatus(Guid id, string key, string nome, string dominio) : base(id, key, nome, dominio) { }

    public static ClienteStatus Get(string key) =>
        key.ToLower() switch
        {
            "ativo" => Ativo,
            "inativo" => Inativo,
            _ => throw new ArgumentOutOfRangeException($"Cliente Status inválido: {key}")
        };

    public static ClienteStatus Get(Guid id) =>
        id == Ativo.Id ? Ativo :
        id == Inativo.Id ? Inativo :
        throw new ArgumentOutOfRangeException($"Cliente Status inválido: {id}");

    public static readonly ClienteStatus Ativo = ("b8e2a1c4-6f3d-4e2b-9a7c-8d2e4f5b1a6c", "Ativo", "Ativo", "ClienteStatus");
    public static readonly ClienteStatus Inativo = ("c7a3d2e1-5b6f-4c9d-8f0a-3b2c4d5e7f8a", "Inativo", "Inativo", "ClienteStatus");


    public static implicit operator ClienteStatus((string Id, string Key, string Nome, string Dominio) data) =>
        new(new Guid(data.Id), data.Key, data.Nome, data.Dominio);
}
