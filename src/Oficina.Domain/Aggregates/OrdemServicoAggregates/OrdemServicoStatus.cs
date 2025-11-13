
using Oficina.Domain.Enumerations;

namespace Oficina.Domain.Aggregates.OrdemServicoAggregates;

public record OrdemServicoStatus : DadoDominio
{
    private OrdemServicoStatus() : base() { }

    private OrdemServicoStatus(Guid id, string key, string nome, string dominio) : base(id, key, nome, dominio) { }

    public static OrdemServicoStatus Get(string key) =>
        key.ToLower() switch
        {
            "aberto" => Aberto,
            "fechado" => Fechado,
            "inativo" => Inativo,
            _ => throw new ArgumentOutOfRangeException($"Ordem Serviço Status inválido: {key}")
        };

    public static OrdemServicoStatus Get(Guid id) =>
        id == Aberto.Id ? Aberto :
        id == Fechado.Id ? Fechado :
        id == Inativo.Id ? Inativo :
        throw new ArgumentOutOfRangeException($"Ordem Serviço Status inválido: {id}");

    public static readonly OrdemServicoStatus Aberto = ("1d961034-70e9-4651-9504-2e24d04ce3e0", "Aberto", "Aberto", "OrdemServico");
    public static readonly OrdemServicoStatus Fechado = ("7019f9f9-72ed-4fc6-88c6-8d125665ff2a", "Fechado", "Fechado", "OrdemServico");
    public static readonly OrdemServicoStatus Inativo = ("37be3253-ae1c-4650-b0e5-d0047c313e6f", "Inativo", "Inativo", "OrdemServico");


    public static implicit operator OrdemServicoStatus((string Id, string Key, string Nome, string Dominio) data) =>
        new(new Guid(data.Id), data.Key, data.Nome, data.Dominio);
}
