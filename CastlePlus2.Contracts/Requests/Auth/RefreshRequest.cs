namespace CastlePlus2.Contracts.Requests.Auth
{
    public class RefreshRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string? DeviceInfo { get; set; }
    }
}