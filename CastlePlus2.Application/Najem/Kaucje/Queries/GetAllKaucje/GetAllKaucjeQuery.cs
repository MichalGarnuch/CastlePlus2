using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Kaucje.Queries.GetAllKaucje
{
    public class GetAllKaucjeQuery : IRequest<List<KaucjaDto>>
    {
    }
}
