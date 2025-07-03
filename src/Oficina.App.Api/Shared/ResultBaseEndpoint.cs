using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Core;
using FastEndpoints;

namespace Oficina.App.Api.Shared;

public abstract class ResultBaseEndpoint<TRequest, TResponse>(
    IUseCase<TRequest, TResponse> useCase) 
    : Endpoint<TRequest, Result<TResponse>>
    where TRequest : notnull
{
    public override async Task HandleAsync(TRequest req, CancellationToken ct)
    {
        Response = await useCase
            .Execute(req, ct);
    }
}