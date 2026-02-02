using MediatR;

namespace CastlePlus2.Application.Auth.RequestAccess.Commands
{
    public sealed class CreateRequestAccessCommand : IRequest<int>
    {
        public string FullName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string? Login { get; init; }
        public string? Phone { get; init; }
        public string Department { get; init; } = string.Empty;
        public string Justification { get; init; } = string.Empty;
    }
}