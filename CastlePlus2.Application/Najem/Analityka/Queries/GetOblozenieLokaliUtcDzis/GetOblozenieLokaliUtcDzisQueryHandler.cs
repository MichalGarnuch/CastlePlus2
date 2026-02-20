using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Analityka.Queries.GetOblozenieLokaliUtcDzis
{
    public sealed class GetOblozenieLokaliUtcDzisQueryHandler
        : IRequestHandler<GetOblozenieLokaliUtcDzisQuery, IReadOnlyList<OblozenieLokaluDto>>
    {
        private readonly INajemAnalitykaQueryService _najemAnalitykaQueryService;

        public GetOblozenieLokaliUtcDzisQueryHandler(INajemAnalitykaQueryService najemAnalitykaQueryService)
        {
            _najemAnalitykaQueryService = najemAnalitykaQueryService;
        }

        public Task<IReadOnlyList<OblozenieLokaluDto>> Handle(GetOblozenieLokaliUtcDzisQuery request, CancellationToken ct)
            => _najemAnalitykaQueryService.GetOblozenieLokaliUtcDzisAsync(ct);
    }
}
