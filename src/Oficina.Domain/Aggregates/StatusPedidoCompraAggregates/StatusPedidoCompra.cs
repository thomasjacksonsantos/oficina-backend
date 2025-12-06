
using Oficina.Domain.SeedWork;
using Oficina.Domain.ValueObjects;

namespace Oficina.Domain.Aggregates.StatusPedidoCompraAggregates;

public class StatusPedidoCompra
{
    public int Id { get; private set; }
    public string Descricao { get; private set; }
    public Guid StatusPedidoCompraStatusId { get; private set; }
    public StatusPedidoCompraStatus StatusPedidoCompraStatus { get; private set; }
    public DataHora Criado { get; private set; }
    public DataHora Atualizado { get; private set; }
#pragma warning disable CS8618
    private StatusPedidoCompra() { }
#pragma warning restore CS8618

    private StatusPedidoCompra(
        string descricao
    )
    {
        Descricao = descricao;
        StatusPedidoCompraStatusId = StatusPedidoCompraStatus.Ativo.Id;
        StatusPedidoCompraStatus = StatusPedidoCompraStatus.Ativo;
        Criado = DateTime.Now;
        Atualizado = DateTime.Now;
    }

    public void Ativar()
    {
        StatusPedidoCompraStatusId = StatusPedidoCompraStatus.Ativo.Id;
        StatusPedidoCompraStatus = StatusPedidoCompraStatus.Ativo;
        Atualizado = DateTime.Now;
    }

    public void Desativar()
    {
        StatusPedidoCompraStatusId = StatusPedidoCompraStatus.Inativo.Id;
        StatusPedidoCompraStatus = StatusPedidoCompraStatus.Inativo;
        Atualizado = DateTime.Now;
    }   

    public Result<StatusPedidoCompra> Atualizar(
        string descricao
    )
    {
        var result = new Result<StatusPedidoCompra>();

        if (string.IsNullOrWhiteSpace(descricao))
            result.WithError(Erro.ValorInvalido($"{nameof(StatusPedidoCompra)}.{nameof(descricao)}", "Descrição é obrigatório."));

        if (result.IsFailed)
            return result;

        Descricao = descricao;
        Atualizado = DateTime.Now;

        return Result<StatusPedidoCompra>.Success(this);
    }

    public static Result<StatusPedidoCompra> Criar(
        string descricao
    )
    {
        var result = new Result<StatusPedidoCompra>();

        if (string.IsNullOrWhiteSpace(descricao))
            result.WithError(Erro.ValorInvalido($"{nameof(StatusPedidoCompra)}.{nameof(descricao)}", "Descrição é obrigatório."));        
        
        if (result.IsFailed)
            return result;

        var statusPedidoCompra = new StatusPedidoCompra(
            descricao
        );

        return Result<StatusPedidoCompra>.Success(statusPedidoCompra);
    }
}