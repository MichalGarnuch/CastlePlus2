using CastlePlus2.Contracts.DTOs.Dashboard;
using MediatR;

namespace CastlePlus2.Application.Dashboard.Najem.Queries.GetDashboardV1Najem
{
    public sealed record GetDashboardV1NajemQuery() : IRequest<DashboardV1NajemDto>;
}
