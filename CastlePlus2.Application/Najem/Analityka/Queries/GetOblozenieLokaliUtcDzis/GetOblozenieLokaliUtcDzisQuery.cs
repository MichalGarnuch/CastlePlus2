using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Analityka.Queries.GetOblozenieLokaliUtcDzis
{
    public sealed record GetOblozenieLokaliUtcDzisQuery : IRequest<IReadOnlyList<OblozenieLokaluDto>>;
}