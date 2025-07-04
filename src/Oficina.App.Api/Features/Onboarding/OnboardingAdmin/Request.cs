
using Oficina.App.Api.Shared;
using Oficina.Domain.ValueObjects;

namespace Oficina.App.Api.Features.Onboarding.OnboardingAdmin;

public sealed record OnboardingAdminRequest(
    string Nome,
    string TipoDocumento,
    string Documento,
    string Sexo,
    DateTime DataNascimento,
    ICollection<OnboardingAdminContato> Contatos,
    OnboardingAdminEndereco Endereco
) : AuthRequest;


public record OnboardingAdminContato(
    string DDD,
    string Numero,
    string TipoTelefone
);

public record OnboardingAdminEndereco(
    string Pais,
    string Estado,
    string Cidade,
    string Logradouro,
    string Bairro,
    string Complemento,
    string Cep,
    string Numero
);