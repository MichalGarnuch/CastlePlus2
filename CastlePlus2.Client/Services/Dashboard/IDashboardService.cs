using CastlePlus2.Contracts.DTOs.Dashboard;

namespace CastlePlus2.Client.Services.Dashboard
{
    public interface IDashboardService
    {
        Task<NajemDashboardDto> GetNajemDashboardAsync(CancellationToken ct = default);
    }
}