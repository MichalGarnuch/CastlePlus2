using MediatR;

namespace CastlePlus2.Application.Auth.RequestAccess.Commands
{
    public sealed class RejectRequestAccessCommand : IRequest
    {
        public int RequestAccessId { get; init; }
        public string RejectedBy { get; init; } = string.Empty;
        public string? Reason { get; init; }
    }
}