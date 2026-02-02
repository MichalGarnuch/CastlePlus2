using CastlePlus2.Contracts.DTOs.Auth;
using CastlePlus2.Domain.Entities.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.RequestAccess.Queries
{
    public sealed class GetRequestAccessListQuery : IRequest<RequestAccessDto[]>
    {
        public GetRequestAccessListQuery(RequestAccessStatus status)
        {
            Status = status;
        }

        public RequestAccessStatus Status { get; }
    }
}