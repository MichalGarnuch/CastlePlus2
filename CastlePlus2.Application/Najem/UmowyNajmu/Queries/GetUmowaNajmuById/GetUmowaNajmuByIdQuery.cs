using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.UmowyNajmu.Queries.GetUmowaNajmuById
{
    public class GetUmowaNajmuByIdQuery : IRequest<UmowaNajmuDto?>
    {
        public GetUmowaNajmuByIdQuery(Guid id) => Id = id;
        public Guid Id { get; }
    }
}
