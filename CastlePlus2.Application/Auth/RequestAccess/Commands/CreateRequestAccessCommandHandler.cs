using System;
using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Application.Interfaces.Notifications;
using CastlePlus2.Domain.Entities.Auth;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using RequestAccessEntity = CastlePlus2.Domain.Entities.Auth.RequestAccess;

namespace CastlePlus2.Application.Auth.RequestAccess.Commands
{
    public sealed class CreateRequestAccessCommandHandler : IRequestHandler<CreateRequestAccessCommand, int>
    {
        private readonly IAccessRequestRepository _requestRepository;
        private readonly IEmailSender _emailSender;
        private readonly IAppUrlProvider _urlProvider;

        public CreateRequestAccessCommandHandler(
            IAccessRequestRepository requestRepository,
            IEmailSender emailSender,
            IAppUrlProvider urlProvider)
        {
            _requestRepository = requestRepository;
            _emailSender = emailSender;
            _urlProvider = urlProvider;
        }

        public async Task<int> Handle(CreateRequestAccessCommand request, CancellationToken ct)
        {
            var hasPending = await _requestRepository.PendingExistsByEmailOrLoginAsync(request.Email, request.Login, ct);
            if (hasPending)
            {
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(CreateRequestAccessCommand.Email),
                        "Istnieje już aktywne zgłoszenie dla tego adresu email lub loginu.")
                });
            }

            var utcNow = DateTime.UtcNow;

            var entity = new RequestAccessEntity
            {
                FullName = request.FullName,
                Email = request.Email,
                Login = string.IsNullOrWhiteSpace(request.Login) ? null : request.Login,
                Phone = string.IsNullOrWhiteSpace(request.Phone) ? null : request.Phone,
                Department = request.Department,
                Justification = request.Justification,
                Status = RequestAccessStatus.Pending,
                RequestedBy = request.Email,
                CreatedAtUtc = utcNow,
                UpdatedAtUtc = utcNow
            };

            var id = await _requestRepository.CreateAsync(entity, ct);

            var baseUrl = _urlProvider.GetClientBaseUrl().TrimEnd('/');
            var adminUrl = $"{baseUrl}/admin/request-access";
            var subject = "CastlePlus2: nowe zgłoszenie dostępu";
            var body = $"""
Nowe zgłoszenie dostępu:

Imię i nazwisko: {entity.FullName}
Email: {entity.Email}
Login: {entity.Login ?? "-"}
Telefon: {entity.Phone ?? "-"}
Dział/rola biznesowa: {entity.Department}
Uzasadnienie: {entity.Justification}

Panel administracyjny: {adminUrl}
""";

            await _emailSender.SendAsync(Array.Empty<string>(), subject, body, ct);
            return id;
        }
    }
}
