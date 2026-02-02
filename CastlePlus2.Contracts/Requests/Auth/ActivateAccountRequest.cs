namespace CastlePlus2.Contracts.Requests.Auth
{
    public sealed class ActivateAccountRequest
    {
        public string Token { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}