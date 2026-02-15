using CastlePlus2.Contracts.DTOs.Dashboard;
using CastlePlus2.Contracts.Requests.Dashboard;
using MediatR;

namespace CastlePlus2.Application.Dashboard.Najem.Queries.GetNajemPowerDashboard
{
    public sealed record GetNajemPowerDashboardQuery(GetNajemPowerDashboardRequest Request) : IRequest<NajemPowerDashboardDto>;
}