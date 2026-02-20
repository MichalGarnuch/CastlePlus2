using CastlePlus2.Contracts.DTOs.Najem;
using CastlePlus2.Contracts.Requests.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Analityka.Queries.GetRaportNajmuZaMiesiac
{
    public sealed record GetRaportNajmuZaMiesiacQuery(GetRaportNajmuZaMiesiacRequest Request) : IRequest<IReadOnlyList<RaportNajmuZaMiesiacRowDto>>;
}