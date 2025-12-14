using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Przylacza.Queries.GetPrzylaczeById
{
    public class GetPrzylaczeByIdQuery : IRequest<PrzylaczeDto?>
    {
        public long IdPrzylacza { get; }

        public GetPrzylaczeByIdQuery(long idPrzylacza)
        {
            IdPrzylacza = idPrzylacza;
        }
    }
}
