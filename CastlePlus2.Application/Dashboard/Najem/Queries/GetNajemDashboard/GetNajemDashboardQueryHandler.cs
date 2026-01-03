using CastlePlus2.Application.Interfaces.Dashboard;
using CastlePlus2.Contracts.DTOs.Dashboard;
using MediatR;

namespace CastlePlus2.Application.Dashboard.Najem.Queries.GetNajemDashboard
{
    public sealed class GetNajemDashboardQueryHandler : IRequestHandler<GetNajemDashboardQuery, NajemDashboardDto>
    {
        private readonly INajemDashboardQueryService _dashboardQueryService;

        public GetNajemDashboardQueryHandler(INajemDashboardQueryService dashboardQueryService)
        {
            _dashboardQueryService = dashboardQueryService;
        }

        public Task<NajemDashboardDto> Handle(GetNajemDashboardQuery request, CancellationToken ct)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var endDate = today.AddDays(30);
            return _dashboardQueryService.GetNajemDashboardAsync(today, endDate, ct);
        }
    }
}