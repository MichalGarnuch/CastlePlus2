using MediatR;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Login
{
    public sealed class LoginCommand : IRequest<LoginResult>
    {
        public string LoginOrEmail { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string? DeviceInfo { get; init; }
    }
}
