
using Oficina.App.Api.Shared;

namespace Oficina.App.Api.Features.Onboarding.OnboardingAdmin;

public sealed record OnboardingAdminRequest(
    string Nome,
    string TipoDocumento,
    string Documento,
    string Sexo,
    string Email,
    string Senha,
    string ConfirmarSenha,
    DateTime DataNascimento,
    ICollection<OnboardingAdminContato> Contatos,
    OnboardingAdminEndereco Endereco
);


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