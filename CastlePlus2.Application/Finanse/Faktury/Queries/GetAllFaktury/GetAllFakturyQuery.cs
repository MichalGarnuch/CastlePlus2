using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Faktury.Queries.GetAllFaktury
{
    public record GetAllFakturyQuery() : IRequest<List<FakturaDto>>;
}
