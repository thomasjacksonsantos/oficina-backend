
namespace Oficina.Infrastructure.IO;

public record EmailParams(
    string To,
    string Subject,
    string Body
);

public interface IEmailSend
{
    ValueTask<(bool Success, string Message)> SendAsync(
       EmailParams cepParams,
       CancellationToken ct = default
   );
}