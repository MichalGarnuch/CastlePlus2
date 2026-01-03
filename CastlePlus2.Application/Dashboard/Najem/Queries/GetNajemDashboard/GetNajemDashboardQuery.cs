using CastlePlus2.Contracts.DTOs.Dashboard;
using MediatR;

namespace CastlePlus2.Application.Dashboard.Najem.Queries.GetNajemDashboard
{
    public sealed record GetNajemDashboardQuery(int ZakresDni = 30, bool TylkoBezterminowe = false) : IRequest<NajemDashboardDto>;
}