using MediatR;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Refresh
{
    public sealed class RefreshCommand : IRequest<RefreshResult>
    {
        public string RefreshToken { get; init; } = string.Empty;
        public string? DeviceInfo { get; init; }
    }
}
