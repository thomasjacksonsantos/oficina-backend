using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.StatusPedidoCompraAggregates;

public record StatusPedidoCompraStatus : DadoDominio
{
private StatusPedidoCompraStatus() : base() { }
    private StatusPedidoCompraStatus(Guid id, string key, string nome, string dominio) : base(id, key, nome, dominio) { }

    public static StatusPedidoCompraStatus Get(string key) =>
        key.ToLower() switch
        {
            "ativo" => Ativo,
            "inativo" => Inativo,
            _ => throw new ArgumentOutOfRangeException($"Marca Produto Status inválido: {key}")
        };

    public static StatusPedidoCompraStatus Get(Guid id) =>
        id == Ativo.Id ? Ativo :
        id == Inativo.Id ? Inativo :
        throw new ArgumentOutOfRangeException($"Marca Produto Status inválido: {id}");

    public static readonly StatusPedidoCompraStatus Ativo = ("2b8e4c1a-7d3f-4a6b-9c2e-5f1b8a3c6d7e", "Ativo", "Ativo", "StatusPedidoCompraStatus");
    public static readonly StatusPedidoCompraStatus Inativo = ("6a1c3e2b-9d7f-4c8a-8b2e-3d5f7a1b2c4e", "Inativo", "Inativo", "StatusPedidoCompraStatus");

    public static implicit operator StatusPedidoCompraStatus((string Id, string Key, string Nome, string Dominio) data) =>
        new(new Guid(data.Id), data.Key, data.Nome, data.Dominio);
}
