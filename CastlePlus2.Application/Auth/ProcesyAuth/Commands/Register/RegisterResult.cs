using CastlePlus2.Contracts.DTOs.Auth;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Register
{
    public sealed class RegisterResult
    {
        public AuthTokensDto Tokens { get; init; } = new();
    }
}
