using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.VeiculoAggregates;

public record VeiculoStatus : DadoDominio
{
private VeiculoStatus() : base() { }

    private VeiculoStatus(Guid id, string key, string nome, string dominio) : base(id, key, nome, dominio) { }
    public static VeiculoStatus Get(string key) =>
        key.ToLower() switch
        {
            "ativo" => Ativo,
            "inativo" => Inativo,
            _ => throw new ArgumentOutOfRangeException($"Veiculo Status inválido: {key}")
        };

    public static VeiculoStatus Get(Guid id) =>
        id == Ativo.Id ? Ativo :
        id == Inativo.Id ? Inativo :
        throw new ArgumentOutOfRangeException($"Veiculo Status inválido: {id}");

    public static readonly VeiculoStatus Ativo = ("9cc10d5a-0355-496b-a116-537c9aaffbdb", "Ativo", "Ativo", "VeiculoStatus");
    public static readonly VeiculoStatus Inativo = ("632fa768-d5e4-455c-a0f3-85272af95b39", "Inativo", "Inativo", "VeiculoStatus");


    public static implicit operator VeiculoStatus((string Id, string Key, string Nome, string Dominio) data) =>
        new(new Guid(data.Id), data.Key, data.Nome, data.Dominio);
}
