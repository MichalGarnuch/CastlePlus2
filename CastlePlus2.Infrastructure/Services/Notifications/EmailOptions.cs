namespace CastlePlus2.Infrastructure.Services.Notifications
{
    public sealed class EmailOptions
    {
        public bool Enabled { get; set; }
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 25;
        public string? User { get; set; }
        public string? Pass { get; set; }
        public string From { get; set; } = string.Empty;
        public string[] AdminRecipients { get; set; } = Array.Empty<string>();
        public bool UseSsl { get; set; } = true;
    }
}