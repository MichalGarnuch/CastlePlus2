using CastlePlus2.Contracts.DTOs.Najem;
using MediatR;

namespace CastlePlus2.Application.Najem.SkladnikiCzynszu.Queries.GetSkladnikCzynszuById
{
    public class GetSkladnikCzynszuByIdQuery : IRequest<SkladnikCzynszuDto?>
    {
        public GetSkladnikCzynszuByIdQuery(long id) => Id = id;
        public long Id { get; }
    }
}
