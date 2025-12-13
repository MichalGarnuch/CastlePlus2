using AutoMapper;
using CastlePlus2.Application.Interfaces;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.PrzypisaniaAdresow.Commands.CreatePrzypisanieAdresu
{
    public sealed class CreatePrzypisanieAdresuCommandHandler
        : IRequestHandler<CreatePrzypisanieAdresuCommand, PrzypisanieAdresuDto>
    {
        private readonly IPrzypisanieAdresuRepository _repo;
        private readonly IMapper _mapper;

        public CreatePrzypisanieAdresuCommandHandler(
            IPrzypisanieAdresuRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PrzypisanieAdresuDto> Handle(
            CreatePrzypisanieAdresuCommand request,
            CancellationToken cancellationToken)
        {
            // Zasada bezpieczeństwa: jedna aktywna pozycja na Encję (DoDnia == null).
            // Jeśli już istnieje aktywne przypisanie, zamykamy je w dniu poprzedzającym nowe OdDnia.
            var aktywne = await _repo.GetAktywneDlaEncjiAsync(request.IdEncji, cancellationToken);
            if (aktywne != null)
            {
                // Zamknięcie poprzedniego okresu (prosty, czytelny wariant).
                aktywne.DoDnia = request.OdDnia.AddDays(-1);
            }

            var entity = new PrzypisanieAdresu
            {
                IdEncji = request.IdEncji,
                IdAdresu = request.IdAdresu,
                OdDnia = request.OdDnia,
                DoDnia = null
            };

            await _repo.AddAsync(entity, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PrzypisanieAdresuDto>(entity);
        }
    }
}
