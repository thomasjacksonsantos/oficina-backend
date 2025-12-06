

using Oficina.Domain.Aggregates.StatusPedidoCompraAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.App.Api.Features.StatusPedidosCompras.CadastrarStatusPedidoCompra;

public sealed class UseCase(
    IRepository<StatusPedidoCompra> statusPedidoCompraRepository,
    IFluentQuery fluentQuery,
    IUnitOfWork unitOfWork
)
    : IUseCase<CadastrarStatusPedidoCompraRequest, CadastrarStatusPedidoCompraResponse>
{
    public async Task<Result<CadastrarStatusPedidoCompraResponse>> Execute(
        CadastrarStatusPedidoCompraRequest input,
        CancellationToken ct = default
    )
    {
        var statusPedidoCompraExists = await fluentQuery.For<StatusPedidoCompra>()
            .WithPredicate(x =>
                x.Descricao == input.Descricao
            )
            .CountAsync();

        if (statusPedidoCompraExists > 0)
            return Result.Fail(Erro.ValorInvalido($"{nameof(StatusPedidoCompra)}", "Valor da descrição já consta cadastrado no sistema."));
        var statusPedidoCompra = StatusPedidoCompra.Criar(
            input.Descricao
        );

        if (statusPedidoCompra.IsFailed)
            return Result.Fail(statusPedidoCompra.Errors!);

        await statusPedidoCompraRepository.AddAsync(statusPedidoCompra.Value!);
        await unitOfWork.SaveChangesAsync(ct);

        return new CadastrarStatusPedidoCompraResponse(statusPedidoCompra.Value!.Id.EncodeWithSqids());
    }
}
