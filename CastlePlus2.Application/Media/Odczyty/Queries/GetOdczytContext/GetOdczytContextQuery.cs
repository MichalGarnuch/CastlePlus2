using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Odczyty.Queries.GetOdczytContext
{
    public sealed record GetOdczytContextQuery : IRequest<OdczytContextDto>;
}