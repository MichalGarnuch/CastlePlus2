using CastlePlus2.Contracts.DTOs.Auth;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Refresh
{
    public sealed class RefreshResult
    {
        public AuthTokensDto Tokens { get; init; } = new();
    }
}
