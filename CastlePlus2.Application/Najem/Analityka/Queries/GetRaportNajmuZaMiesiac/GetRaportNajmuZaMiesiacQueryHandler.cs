using CastlePlus2.Application.Interfaces.Najem;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Analityka.Queries.GetRaportNajmuZaMiesiac
{
    public sealed class GetRaportNajmuZaMiesiacQueryHandler
        : IRequestHandler<GetRaportNajmuZaMiesiacQuery, IReadOnlyList<RaportNajmuZaMiesiacRowDto>>
    {
        private readonly INajemAnalitykaQueryService _najemAnalitykaQueryService;

        public GetRaportNajmuZaMiesiacQueryHandler(INajemAnalitykaQueryService najemAnalitykaQueryService)
        {
            _najemAnalitykaQueryService = najemAnalitykaQueryService;
        }

        public Task<IReadOnlyList<RaportNajmuZaMiesiacRowDto>> Handle(GetRaportNajmuZaMiesiacQuery request, CancellationToken ct)
            => _najemAnalitykaQueryService.GetRaportNajmuZaMiesiacAsync(request.Request.Rok, request.Request.Miesiac, ct);
    }
}