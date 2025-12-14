using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.PrzedmiotyNajmu.Queries.GetPrzedmiotNajmuById
{
    public class GetPrzedmiotNajmuByIdQuery : IRequest<PrzedmiotNajmuDto?>
    {
        public GetPrzedmiotNajmuByIdQuery(long id) => Id = id;
        public long Id { get; }
    }
}
