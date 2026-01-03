using CastlePlus2.Contracts.DTOs.Dashboard;
using MediatR;

namespace CastlePlus2.Application.Dashboard.Najem.Queries.GetNajemDashboard
{
    public sealed record GetNajemDashboardQuery : IRequest<NajemDashboardDto>;
}