
namespace Oficina.App.Api.Features.Onboarding.OnboardingAdmin;

public sealed record OnboardingAdminRequest(
    string Nome,
    string TipoDocumento,
    string Documento,
    string Sexo,
    string Email,
    string Senha,
    string ConfirmarSenha,
    ICollection<OnboardingAdminContato> Contatos,
    OnboardingLoja Loja,
    DateTime DataNascimento
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

public record OnboardingLoja(
    string NomeFantasia,
    string RazaoSocial,
    string Cnpj,
    string InscricaoEstadual,
    string Site,
    string LogoTipo,
    OnboardingAdminEndereco Endereco,
    ICollection<OnboardingAdminContato> Contatos
);