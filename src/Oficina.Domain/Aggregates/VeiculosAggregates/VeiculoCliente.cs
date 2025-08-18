
using Oficina.Domain.Aggregates.ClienteAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.VeiculoAggregates;

public class VeiculoCliente
{
    public int Id { get; private set; }
    public int VeiculoId { get; private set; }
    public Veiculo Veiculo { get; private set; }
    public int ClienteId { get; private set; }
    public Cliente Cliente { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }

#pragma warning disable CS8618 // Propriedade não inicializada
    private VeiculoCliente() { }
#pragma warning restore CS8618

    public VeiculoCliente(
        Veiculo veiculo,
        Cliente cliente)
    {
        VeiculoId = veiculo.Id;
        Veiculo = veiculo;
        ClienteId = cliente.Id;
        Cliente = cliente;
        Criado = DataHora.Criar().Value!;
        Atualizado = DataHora.Criar().Value!;
    }

    public static Result<VeiculoCliente> Criar(
        Veiculo veiculo,
        Cliente cliente)
    {
        var erros = new List<string>();

        if (veiculo == null)
            erros.Add("Veículo não pode ser nulo.");

        if (cliente == null)
            erros.Add("Cliente não pode ser nulo.");

        if (erros.Any())
            return Result.Fail(string.Join(" ", erros));

        var veiculoCliente = new VeiculoCliente(
            veiculo!,
            cliente!
        );

        return Result.Success(veiculoCliente);
    }
}