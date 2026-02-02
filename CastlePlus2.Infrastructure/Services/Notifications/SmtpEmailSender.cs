using System.Net;
using System.Net.Mail;
using System.Linq;
using CastlePlus2.Application.Interfaces.Notifications;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CastlePlus2.Infrastructure.Services.Notifications
{
    public sealed class SmtpEmailSender : IEmailSender
    {
        private readonly EmailOptions _options;
        private readonly ILogger<SmtpEmailSender> _logger;

        public SmtpEmailSender(IOptions<EmailOptions> options, ILogger<SmtpEmailSender> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task SendAsync(string[] recipients, string subject, string body, CancellationToken ct)
        {
            var targetRecipients = (recipients is { Length: > 0 }
                    ? recipients
                    : _options.AdminRecipients)
                .Where(email => !string.IsNullOrWhiteSpace(email))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            if (targetRecipients.Length == 0)
            {
                _logger.LogWarning("Brak odbiorców emaila. Temat: {Subject}", subject);
                return;
            }

            if (!_options.Enabled)
            {
                _logger.LogInformation(
                    "Email wyłączony. Temat: {Subject}. Odbiorcy: {Recipients}. Treść: {Body}",
                    subject,
                    string.Join(",", targetRecipients),
                    body);
                return;
            }

            using var message = new MailMessage
            {
                From = new MailAddress(_options.From),
                Subject = subject,
                Body = body
            };

            foreach (var recipient in targetRecipients)
            {
                message.To.Add(recipient);
            }

            using var client = new SmtpClient(_options.Host, _options.Port)
            {
                EnableSsl = _options.UseSsl
            };

            if (!string.IsNullOrWhiteSpace(_options.User))
            {
                client.Credentials = new NetworkCredential(_options.User, _options.Pass);
            }

            await client.SendMailAsync(message, ct);
        }
    }
}