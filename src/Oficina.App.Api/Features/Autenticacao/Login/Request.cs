
namespace Oficina.App.Api.Features.Autenticacao.Login;

public sealed record LoginRequest(
    string Email,
    string Senha
);