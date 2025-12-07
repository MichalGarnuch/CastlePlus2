using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Adresy.Queries.GetAdresById
{
    /// <summary>
    /// Zapytanie o pojedynczy adres po IdAdresu.
    /// </summary>
    public record GetAdresByIdQuery(long IdAdresu) : IRequest<AdresDto?>;
}

