using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.PlanoAggregates;

public record PlanoStatus : DadoDominio
{
private PlanoStatus() : base() { }

    private PlanoStatus(Guid id, string key, string nome, string dominio) : base(id, key, nome, dominio) { }

    public static PlanoStatus Get(string key) =>
        key.ToLower() switch
        {
            "ativo" => Ativo,
            "inativo" => Inativo,
            _ => throw new ArgumentOutOfRangeException($"Plano Status inválido: {key}")
        };

    public static PlanoStatus Get(Guid id) =>
        id == Ativo.Id ? Ativo :
        id == Inativo.Id ? Inativo :
        throw new ArgumentOutOfRangeException($"Plano Status inválido: {id}");

    public static readonly PlanoStatus Ativo = ("e3b8c6a2-2f4a-4b9d-8e7a-1c2d3e4f5a6b", "Ativo", "Ativo", "Plano");
    public static readonly PlanoStatus Inativo = ("a7f2d1c3-5b6e-4c8d-9f0a-2b3c4d5e6f7a", "Inativo", "Inativo", "Plano");


    public static implicit operator PlanoStatus((string Id, string Key, string Nome, string Dominio) data) =>
        new(new Guid(data.Id), data.Key, data.Nome, data.Dominio);
}
