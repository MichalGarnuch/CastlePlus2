using CastlePlus2.Contracts.DTOs.Auth;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Login
{
    public class LoginResult
    {
        public AuthTokensDto Tokens { get; set; } = new AuthTokensDto();
    }
}