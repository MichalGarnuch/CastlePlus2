using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.Przylacza.Queries.GetPrzylaczeById
{
    public class GetPrzylaczeByIdQueryHandler : IRequestHandler<GetPrzylaczeByIdQuery, PrzylaczeDto?>
    {
        private readonly IPrzylaczeRepository _przylaczeRepo;
        private readonly IRodzajMediumRepository _rodzajRepo;
        private readonly IMapper _mapper;

        public GetPrzylaczeByIdQueryHandler(
            IPrzylaczeRepository przylaczeRepo,
            IRodzajMediumRepository rodzajRepo,
            IMapper mapper)
        {
            _przylaczeRepo = przylaczeRepo;
            _rodzajRepo = rodzajRepo;
            _mapper = mapper;
        }

        public async Task<PrzylaczeDto?> Handle(GetPrzylaczeByIdQuery request, CancellationToken ct)
        {
            if (request.IdPrzylacza <= 0)
                return null;

            var entity = await _przylaczeRepo.GetByIdAsync(request.IdPrzylacza, ct);
            if (entity is null)
                return null;

            var dto = _mapper.Map<PrzylaczeDto>(entity);

            // Dociągamy nazwę słownika (bez logiki w kontrolerze)
            var rodzaj = await _rodzajRepo.GetByIdAsync(entity.KodRodzaju, ct);
            dto.NazwaRodzaju = rodzaj?.Nazwa;

            return dto;
        }
    }
}
