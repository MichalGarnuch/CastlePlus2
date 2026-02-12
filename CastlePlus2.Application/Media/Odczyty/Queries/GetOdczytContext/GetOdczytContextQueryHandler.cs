using System.Threading;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Odczyty.Queries.GetOdczytContext
{
    public class GetOdczytContextQueryHandler : IRequestHandler<GetOdczytContextQuery, OdczytContextDto>
    {
        private readonly ILicznikRepository _licznikRepository;

        public GetOdczytContextQueryHandler(ILicznikRepository licznikRepository)
        {
            _licznikRepository = licznikRepository;
        }

        public async Task<OdczytContextDto> Handle(GetOdczytContextQuery request, CancellationToken ct)
        {
            return new OdczytContextDto
            {
                Liczniki = new List<LicznikOdczytLookupDto>()
            };
        }
    }
}