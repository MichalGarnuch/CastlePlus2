using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Commands.CreateUser
{
    public sealed class CreateUserCommand : IRequest
    {
        public string CreatedBy { get; init; } = string.Empty;
        public string Login { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string ConfirmPassword { get; init; } = string.Empty;
        public string[] RoleCodes { get; init; } = Array.Empty<string>();
    }
}