using MediatR;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Login
{
    public class LoginCommand : IRequest<LoginResult>
    {
        public string LoginOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? DeviceInfo { get; set; }
    }
}