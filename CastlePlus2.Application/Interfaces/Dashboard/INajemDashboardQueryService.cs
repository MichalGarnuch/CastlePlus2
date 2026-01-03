using CastlePlus2.Contracts.DTOs.Dashboard;

namespace CastlePlus2.Application.Interfaces.Dashboard
{
    public interface INajemDashboardQueryService
    {
        Task<NajemDashboardDto> GetNajemDashboardAsync(
            DateOnly today,
            DateOnly koniecOkresu,
            CancellationToken ct);
    }
}
