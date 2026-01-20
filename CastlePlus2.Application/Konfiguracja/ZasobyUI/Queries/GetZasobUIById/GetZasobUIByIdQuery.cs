using CastlePlus2.Contracts.DTOs.Konfiguracja;
using MediatR;
using System;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUI.Queries.GetZasobUIById
{
    public record GetZasobUIByIdQuery(Guid IdEncji) : IRequest<ZasobUIDto?>;
}