namespace CastlePlus2.Contracts.Requests.Auth
{
    public sealed class CreateRequestAccessRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Login { get; set; }
        public string? Phone { get; set; }
        public string Department { get; set; } = string.Empty;
        public string Justification { get; set; } = string.Empty;
    }
}