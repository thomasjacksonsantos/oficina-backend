namespace Oficina.Infrastructure.DataAccess;

public class HttpClientBase(IHttpClientFactory factory, string serviceName)
{
    protected readonly HttpClient Client = factory.CreateClient(serviceName);
}