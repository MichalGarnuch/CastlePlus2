using MediatR;

namespace CastlePlus2.Application.Auth.RequestAccess.Commands
{
    public sealed class ActivateAccountCommand : IRequest
    {
        public string Token { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string ConfirmPassword { get; init; } = string.Empty;
    }
}