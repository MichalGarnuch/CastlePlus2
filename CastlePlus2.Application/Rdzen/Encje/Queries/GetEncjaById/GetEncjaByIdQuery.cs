using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Encje.Queries.GetEncjaById
{
    public class GetEncjaByIdQuery : IRequest<EncjaDto?>
    {
        public Guid Id { get; }

        public GetEncjaByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}