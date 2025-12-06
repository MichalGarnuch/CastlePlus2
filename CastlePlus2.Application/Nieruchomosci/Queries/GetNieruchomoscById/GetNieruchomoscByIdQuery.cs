using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Nieruchomosci.Queries.GetNieruchomoscById
{
    /// <summary>
    /// Zapytanie CQRS o pojedynczą nieruchomość po Id.
    /// 
    /// Zwraca NieruchomoscDto, gotowe do wysłania na frontend.
    /// </summary>
    public class GetNieruchomoscByIdQuery : IRequest<NieruchomoscDto?>
    {
        public Guid Id { get; }

        public GetNieruchomoscByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
