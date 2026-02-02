using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Application.Interfaces.Notifications;
using CastlePlus2.Domain.Entities.Auth;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CastlePlus2.Application.Auth.RequestAccess.Commands
{
    public sealed class RejectRequestAccessCommandHandler : IRequestHandler<RejectRequestAccessCommand>
    {
        private readonly IAccessRequestRepository _requestRepository;
        private readonly IEmailSender _emailSender;

        public RejectRequestAccessCommandHandler(
            IAccessRequestRepository requestRepository,
            IEmailSender emailSender)
        {
            _requestRepository = requestRepository;
            _emailSender = emailSender;
        }

        public async Task Handle(RejectRequestAccessCommand request, CancellationToken ct)
        {
            var entry = await _requestRepository.GetByIdAsync(request.RequestAccessId, ct);
            if (entry is null)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(RejectRequestAccessCommand.RequestAccessId), "Nie znaleziono zgłoszenia.")
                });
            }

            if (entry.Status != RequestAccessStatus.Pending)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(RejectRequestAccessCommand.RequestAccessId), "Zgłoszenie nie jest w statusie Pending.")
                });
            }

            var utcNow = DateTime.UtcNow;
            entry.Status = RequestAccessStatus.Rejected;
            entry.UpdatedAtUtc = utcNow;
            entry.RejectedBy = request.RejectedBy;
            entry.RejectedAtUtc = utcNow;
            entry.RejectionReason = request.Reason;

            await _requestRepository.UpdateAsync(entry, ct);

            var subject = "CastlePlus2: decyzja o dostępie";
            var reasonLine = string.IsNullOrWhiteSpace(request.Reason)
                ? "Decyzja: odrzucono."
                : $"Decyzja: odrzucono. Powód: {request.Reason}";

            var body = $"""
Twoja prośba o dostęp została rozpatrzona.

{reasonLine}
""";

            await _emailSender.SendAsync(new[] { entry.Email }, subject, body, ct);
        }
    }
}