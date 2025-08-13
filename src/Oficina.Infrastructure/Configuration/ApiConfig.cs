namespace Oficina.Infrastructure.Configuration;

#pragma warning disable CS8618
public class ApiConfig
{
    public BlobStorageConfig BlobStorage { get; set; }
    public  Authentication Authentication { get; set; }
    public Cep Cep { get; set; }
    public Email Email { get; set; }
    public ConviteConfig Convite { get; set; }
}

public class ConviteConfig
{
    public string Subject { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}

public class Email
{
    public string Smtp { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Login { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class Cep
{
    public string ServiceName { get; set; }
    public string Url { get; set; }
    public string Token { get; set; }
}

public class BlobStorageConfig
{
    public string BlobStorageConnectionString { get; set; }
    public string ContainerName { get; set; }
}

public class Authentication
{
    public string TokenUri { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
}

#pragma warning restore CS8618