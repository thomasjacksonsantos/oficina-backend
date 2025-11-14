

using System.Net.Http.Json;
using System.Text.Json;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Options;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Configuration;
using Oficina.Infrastructure.Core;

namespace Oficina.App.Api.Features.Usuarios.ValidarEmailExistente;

public sealed class UseCase(
    IOptions<ApiConfig> options
)
    : IUseCase<ValidarEmailExistenteRequest, ValidarEmailExistenteResponse>
{
    private readonly ApiConfig apiConfig = options.Value;

    public async Task<Result<ValidarEmailExistenteResponse>> Execute(
        ValidarEmailExistenteRequest input,
        CancellationToken ct = default
    )
    {

        try
        {
            var records = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(input.Valor);
            return Result.Success(new ValidarEmailExistenteResponse(
                EmailExistente: true,
                Mensagem: "Email já cadastrado."
            ));
        }
        catch (FirebaseAuthException ex) when (ex.AuthErrorCode == AuthErrorCode.UserNotFound)
        {
            return Result.Success(new ValidarEmailExistenteResponse(
                EmailExistente: false,
                Mensagem: "Email não cadastrado."
            ));
        }
    }

    private class FirebaseSignUpResponse
    {
        public FirebaseError? error { get; set; }
        public string localId { get; set; } = string.Empty;
    }

    private class FirebaseError
    {
        public FirebaseErrorDetail[] errors { get; set; } = Array.Empty<FirebaseErrorDetail>();
        public string message { get; set; } = string.Empty;
    }

    private class FirebaseErrorDetail
    {
        public string message { get; set; } = string.Empty;
    }
}
