using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Contracts.DTOs.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.RequestAccess.Queries
{
    public sealed class GetRequestAccessListQueryHandler : IRequestHandler<GetRequestAccessListQuery, RequestAccessDto[]>
    {
        private readonly IAccessRequestRepository _requestRepository;

        public GetRequestAccessListQueryHandler(IAccessRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<RequestAccessDto[]> Handle(GetRequestAccessListQuery request, CancellationToken ct)
        {
            var entries = await _requestRepository.GetByStatusAsync(request.Status, ct);
            return entries.Select(entry => new RequestAccessDto
            {
                IdRequestAccess = entry.IdRequestAccess,
                FullName = entry.FullName,
                Email = entry.Email,
                Login = entry.Login,
                Phone = entry.Phone,
                Department = entry.Department,
                Justification = entry.Justification,
                Status = entry.Status.ToString(),
                CreatedAtUtc = entry.CreatedAtUtc,
                UpdatedAtUtc = entry.UpdatedAtUtc,
                ApprovedBy = entry.ApprovedBy,
                ApprovedAtUtc = entry.ApprovedAtUtc,
                ApprovedLogin = entry.ApprovedLogin,
                ApprovedEmail = entry.ApprovedEmail,
                ApprovedRoleCodes = entry.ApprovedRoleCodes,
                RejectedBy = entry.RejectedBy,
                RejectedAtUtc = entry.RejectedAtUtc,
                RejectionReason = entry.RejectionReason
            })
                .ToArray();
        }
    }
}