using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Budynki.Queries.GetBudynekById
{
    /// <summary>
    /// Zapytanie o pojedynczy budynek po IdEncji.
    /// </summary>
    public class GetBudynekByIdQuery : IRequest<BudynekDto?>
    {
        public Guid Id { get; }

        public GetBudynekByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
