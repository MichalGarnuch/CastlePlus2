using MediatR;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Register
{
    public sealed class RegisterCommand : IRequest<RegisterResult>
    {
        public string Login { get; init; } = string.Empty;
        public string? Email { get; init; }
        public string Password { get; init; } = string.Empty;
        public string? DeviceInfo { get; init; }
    }
}
