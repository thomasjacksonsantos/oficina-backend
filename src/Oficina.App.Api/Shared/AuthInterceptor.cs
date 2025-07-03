
using FastEndpoints;

namespace Oficina.App.Api.Shared;

public class AuthInterceptor<TRequest>(
    IAuthProvider authProvider)
    : IPreProcessor<TRequest> where TRequest : AuthRequest
{
    public Task PreProcessAsync(IPreProcessorContext<TRequest> ctx, CancellationToken ct)
    {
        authProvider.FillAuthRequest(ctx.Request);
        return Task.CompletedTask;
    }
}