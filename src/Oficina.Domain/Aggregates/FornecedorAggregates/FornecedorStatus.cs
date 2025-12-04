using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.FornecedorAggregates;

public record FornecedorStatus : DadoDominio
{
private FornecedorStatus() : base() { }

    private FornecedorStatus(Guid id, string key, string nome, string dominio) : base(id, key, nome, dominio) { }

    public static FornecedorStatus Get(string key) =>
        key.ToLower() switch
        {
            "ativo" => Ativo,
            "inativo" => Inativo,
            _ => throw new ArgumentOutOfRangeException($"Fornecedor Status inválido: {key}")
        };

    public static FornecedorStatus Get(Guid id) =>
        id == Ativo.Id ? Ativo :
        id == Inativo.Id ? Inativo :
        throw new ArgumentOutOfRangeException($"Fornecedor Status inválido: {id}");

    public static readonly FornecedorStatus Ativo = ("7c4b1f2d-1e6b-4971-9c82-4f9d2a7e3b11", "Ativo", "Ativo", "FornecedorStatus");
    public static readonly FornecedorStatus Inativo = ("d2f1a8c3-9b55-4fa7-96f4-8e3c1d77a024", "Inativo", "Inativo", "FornecedorStatus");


    public static implicit operator FornecedorStatus((string Id, string Key, string Nome, string Dominio) data) =>
        new(new Guid(data.Id), data.Key, data.Nome, data.Dominio);
}
