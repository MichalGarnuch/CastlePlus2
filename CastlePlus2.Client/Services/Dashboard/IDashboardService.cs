using CastlePlus2.Contracts.DTOs.Dashboard;

namespace CastlePlus2.Client.Services.Dashboard
{
    public interface IDashboardService
    {
        Task<NajemDashboardDto> GetNajemDashboardAsync(
            int zakresDni = 30,
            CancellationToken ct = default);

        Task<DashboardV1NajemDto> GetDashboardV1NajemAsync(
            CancellationToken ct = default);
    }
}
