using CastlePlus2.Contracts.DTOs.Dashboard;

namespace CastlePlus2.Application.Interfaces.Dashboard
{
    public interface IDashboardV1NajemQueryService
    {
        Task<DashboardV1NajemDto> GetDashboardV1NajemAsync(
            DateOnly today,
            DateOnly koniecOkresu,
            CancellationToken ct);
    }
}