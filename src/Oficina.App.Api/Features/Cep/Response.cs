
namespace Oficina.App.Api.Features.Cep;

public record CepResponse(
    string Pais,
    string Estado,
    string Cidade,
    string Logradouro,
    string Bairro,
    string Complemento,
    string Latitude,
    string Longitude
);

