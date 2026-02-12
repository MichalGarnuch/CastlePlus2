using System;
using CastlePlus2.Contracts.DTOs.Common;
using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Queries.SearchPrzedmiotyNajmuLookupPaged
{
    public sealed record SearchPrzedmiotyNajmuLookupPagedQuery(
        string? Q,
        Guid? IdUmowyNajmu,
        int Page,
        int PageSize
    ) : IRequest<PagedResultDto<PrzedmiotNajmuLookupDto>>;
}