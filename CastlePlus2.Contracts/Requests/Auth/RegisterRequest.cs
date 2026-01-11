namespace CastlePlus2.Contracts.Requests.Auth
{
    public sealed class RegisterRequest
    {
        public string Login { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? DeviceInfo { get; set; }
    }
}
