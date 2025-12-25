using CastlePlus2.Contracts.DTOs.Utrzymanie;
using MediatR;

namespace CastlePlus2.Application.Utrzymanie.ZleceniaPracy.Queries.GetAllZleceniaPracy
{
    public class GetAllZleceniaPracyQuery : IRequest<List<ZleceniePracyDto>>
    {
    }
}