

using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Oficina.Domain.Aggregates.CepAggregates;
using Oficina.Domain.SeedWork;
using Oficina.Infrastructure.Configuration;
using Oficina.Infrastructure.DataAccess;

namespace Oficina.Infrastructure.DomainImplementation.Aggregates.CepAggregates;

public sealed class AwesomeApi(
    IHttpClientFactory factory,
    IOptions<ApiConfig> options
    ) : HttpClientBase(factory, options.Value.Cep.ServiceName), ICepRepository
{
    public async ValueTask<Result<ConsultaCep>> Consultar(CepParams cepParams, CancellationToken ct = default)
    {
        var response = await Client.GetAsync($"json/{cepParams.Cep}?token={options.Value.Cep.Token}", ct);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return Erro.Validacao(
                "Cep",
                 "ConsultaCep",
                 content
            );

        var jObject = JObject.Parse(content);

        return ConsultaCep.Criar(
            "Br",
            jObject["state"]?.ToString() ?? string.Empty,
            jObject["city"]?.ToString() ?? string.Empty,
            jObject["address"]?.ToString() ?? string.Empty,
            jObject["district"]?.ToString() ?? string.Empty,
            jObject["complement"]?.ToString() ?? string.Empty,
            jObject["lat"]?.ToString() ?? string.Empty,
            jObject["lng"]?.ToString() ?? string.Empty
         );
    }
}