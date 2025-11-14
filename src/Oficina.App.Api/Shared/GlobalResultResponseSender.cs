using Oficina.Domain.SeedWork;
using FastEndpoints;
using FluentValidation.Results;

namespace Oficina.App.Api.Shared;

public sealed class GlobalResultResponseSender
    : IGlobalPostProcessor
{
    public Task PostProcessAsync(
        IPostProcessorContext context,
        CancellationToken ct)
    {
        if (context.HttpContext.ResponseStarted() ||
            context.Response is null)
            return Task.CompletedTask;

        var responseType = context
            .Response!
            .GetType();

        if (!responseType.IsGenericType ||
            responseType.GetGenericTypeDefinition() != typeof(Result<>))
            return Task.CompletedTask;

        var isSuccess = (bool)(
            responseType
            .GetProperty("IsSuccess")?
            .GetValue(context.Response) ?? false);

        if (isSuccess)
        {
            var value = responseType
                .GetProperty("Value")?
                .GetValue(context.Response);

            return context
                .HttpContext
                .Response
                .SendAsync(value, cancellation: ct);
        }

        var errosValue = responseType
            .GetProperty("Errors")?
            .GetValue(context.Response);

        if (errosValue is not IReadOnlyCollection<Erro> erros)
            throw new NotImplementedException();

        return context
            .HttpContext
            .Response
            .SendErrorsAsync(
                erros
                    .Select(x => new ValidationFailure(x.Campo, x.Descricao))
                    .ToList(),
                cancellation: ct);
    }
}