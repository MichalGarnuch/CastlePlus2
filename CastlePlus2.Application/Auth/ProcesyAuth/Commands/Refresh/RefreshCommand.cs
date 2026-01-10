using MediatR;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Commands.Refresh
{
    public class RefreshCommand : IRequest<RefreshResult>
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string? DeviceInfo { get; set; }
    }
}