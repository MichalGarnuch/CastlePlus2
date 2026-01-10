using CastlePlus2.Contracts.DTOs.Auth;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Login
{
    public sealed class LoginResult
    {
        public AuthTokensDto Tokens { get; init; } = new();
    }
}
