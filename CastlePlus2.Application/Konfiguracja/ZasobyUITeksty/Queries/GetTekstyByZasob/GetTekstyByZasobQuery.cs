using System;
using System.Collections.Generic;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Queries.GetTekstyByZasob
{
    public sealed record GetTekstyByZasobQuery(Guid IdEncji) : IRequest<List<ZasobUITekstDto>>;
}
