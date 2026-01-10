namespace CastlePlus2.Contracts.Requests.Auth
{
    public class LoginRequest
    {
        public string LoginOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? DeviceInfo { get; set; }
    }
}