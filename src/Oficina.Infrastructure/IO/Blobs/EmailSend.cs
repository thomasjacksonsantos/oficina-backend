using System.Net;
using System.Net.Mail;
using Oficina.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Oficina.Infrastructure.IO.Blobs;

public sealed class EmailSend(IOptions<ApiConfig> config) : IEmailSend
{
    private readonly ApiConfig Config = config.Value;
    public ValueTask<(bool Success, string Message)> SendAsync(
        EmailParams emailParams,
        CancellationToken ct = default
    )
    {
        try
        {
            using var smtpClient = new SmtpClient(Config.Email.Smtp);
            smtpClient.Port = Config.Email.Port;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(Config.Email.Login, Config.Email.Password);

            var emailMessage = new MailMessage
            {
                From = new MailAddress(Config.Email.From, Config.Email.DisplayName),
                Subject = emailParams.Subject,
                Body = emailParams.Body,
                IsBodyHtml = true
            };

            emailMessage.To.Add(
                new MailAddress(
                    emailParams.To
                )
            );

            smtpClient.Send(emailMessage);
        }
        catch (Exception ex)
        {
            return ValueTask.FromResult((false, ex.Message));
        }

        return ValueTask.FromResult((true, "Send email with success."));
    }
}