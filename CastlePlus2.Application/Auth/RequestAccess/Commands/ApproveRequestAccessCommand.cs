using MediatR;

namespace CastlePlus2.Application.Auth.RequestAccess.Commands
{
    public sealed class ApproveRequestAccessCommand : IRequest
    {
        public int RequestAccessId { get; init; }
        public string ApprovedBy { get; init; } = string.Empty;
        public string Login { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string[] RoleCodes { get; init; } = Array.Empty<string>();
    }
}