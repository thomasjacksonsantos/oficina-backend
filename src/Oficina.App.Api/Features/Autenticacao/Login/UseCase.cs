

using System.Net.Http.Json;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Configuration;
using Oficina.Infrastructure.Core;

namespace Oficina.App.Api.Features.Autenticacao.Login;

public sealed class UseCase(
    IHttpClientFactory factory,
    IOptions<ApiConfig> options
)
    : IUseCase<LoginRequest, LoginResponse>
{
    private readonly ApiConfig apiConfig = options.Value;
    public async Task<Result<LoginResponse>> Execute(
        LoginRequest input,
        CancellationToken ct = default
    )
    {
        var requestData = new
        {
            email = input.Email,
            password = input.Senha,
            returnSecureToken = true
        };

        var client = factory.CreateClient(apiConfig.Authentication.ServiceName);

        HttpResponseMessage response = await client.PostAsJsonAsync(
            $"v1/accounts:signInWithPassword?key={apiConfig.Authentication.ApiKey}",
            requestData
        );

        var authToken = await response.Content.ReadFromJsonAsync<AuthToken>() ?? null!;

        return new LoginResponse(authToken.IdToken!);
    }
}
