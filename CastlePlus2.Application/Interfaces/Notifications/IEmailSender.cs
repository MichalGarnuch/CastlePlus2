namespace CastlePlus2.Application.Interfaces.Notifications
{
    public interface IEmailSender
    {
        Task SendAsync(string[] recipients, string subject, string body, CancellationToken ct);
    }
}