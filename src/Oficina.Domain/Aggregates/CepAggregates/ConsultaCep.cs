using Oficina.Domain.SeedWork;

namespace Oficina.Domain.Aggregates.CepAggregates;

public sealed class ConsultaCep
{
    public string Pais { get; set; }
    public string Estado { get; set; }
    public string Cidade { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Complemento { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }

#pragma warning disable CS8618
    public ConsultaCep() { }
#pragma warning restore CS8618

    private ConsultaCep(
        string pais,
        string estado,
        string cidade,
        string logradouro,
        string bairro,
        string complemento,
        string latitude,
        string longitude
    )
    {
        Pais = pais;
        Estado = estado;
        Cidade = cidade;
        Logradouro = logradouro;
        Bairro = bairro;
        Complemento = complemento;
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Result<ConsultaCep> Criar(
        string pais,
        string estado,
        string cidade,
        string logradouro,
        string bairro,
        string complemento,
        string latitude,
        string longitude) => 
        new ConsultaCep(
            pais,
            estado,
            cidade,
            logradouro,
            bairro,
            complemento,
            latitude,
            longitude
        );
}