using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.Kaucje.Queries.GetKaucjaById
{
    public class GetKaucjaByIdQuery : IRequest<KaucjaDto?>
    {
        public GetKaucjaByIdQuery(long id) => Id = id;
        public long Id { get; }
    }
}
