namespace Oficina.Infrastructure.IO;

public record UploadParams(string Filename, string Base64);

public interface IStorage
{
    public ValueTask<string> UploadAsync(UploadParams uploadParams, CancellationToken ct = default);
    public ValueTask<string> DownloadAsync(string filename, CancellationToken ct = default);
    public ValueTask<bool> DeleteAsync(string filename, CancellationToken ct = default);
}