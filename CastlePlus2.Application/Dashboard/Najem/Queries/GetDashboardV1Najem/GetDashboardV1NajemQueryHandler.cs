using CastlePlus2.Application.Interfaces.Dashboard;
using CastlePlus2.Contracts.DTOs.Dashboard;
using MediatR;

namespace CastlePlus2.Application.Dashboard.Najem.Queries.GetDashboardV1Najem
{
    public sealed class GetDashboardV1NajemQueryHandler : IRequestHandler<GetDashboardV1NajemQuery, DashboardV1NajemDto>
    {
        private readonly IDashboardV1NajemQueryService _dashboardQueryService;

        public GetDashboardV1NajemQueryHandler(IDashboardV1NajemQueryService dashboardQueryService)
        {
            _dashboardQueryService = dashboardQueryService;
        }

        public Task<DashboardV1NajemDto> Handle(GetDashboardV1NajemQuery request, CancellationToken ct)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var endDate = today.AddDays(30);

            return _dashboardQueryService.GetDashboardV1NajemAsync(today, endDate, ct);
        }
    }
}