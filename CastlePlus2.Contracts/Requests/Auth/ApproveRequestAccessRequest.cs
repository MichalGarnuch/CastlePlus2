namespace CastlePlus2.Contracts.Requests.Auth
{
    public sealed class ApproveRequestAccessRequest
    {
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string[] RoleCodes { get; set; } = Array.Empty<string>();
    }
}