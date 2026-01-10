using CastlePlus2.Contracts.DTOs.Auth;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Refresh
{
    public class RefreshResult
    {
        public AuthTokensDto Tokens { get; set; } = new AuthTokensDto();
    }
}