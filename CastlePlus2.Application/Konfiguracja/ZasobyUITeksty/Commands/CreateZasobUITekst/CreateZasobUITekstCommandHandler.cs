using AutoMapper;
using CastlePlus2.Application.Common.Exceptions;
using CastlePlus2.Application.Interfaces.Konfiguracja;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using CastlePlus2.Domain.Entities.Konfiguracja;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUITeksty.Commands.CreateZasobUITekst
{
    public class CreateZasobUITekstCommandHandler : IRequestHandler<CreateZasobUITekstCommand, ZasobUITekstDto>
    {
        private readonly IZasobUITekstRepository _repo;
        private readonly IMapper _mapper;

        public CreateZasobUITekstCommandHandler(IZasobUITekstRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ZasobUITekstDto> Handle(CreateZasobUITekstCommand request, CancellationToken ct)
        {
            var existing = await _repo.GetByKeyAsync(request.IdEncji, request.Jezyk, request.Pole, ct);
            if (existing != null)
            {
                throw new BusinessConflictException("Istnieje już tekst dla podanego języka i pola.");
            }

            var entity = new ZasobUITekst
            {
                IdEncji = request.IdEncji,
                Jezyk = request.Jezyk,
                Pole = request.Pole,
                Wartosc = request.Wartosc,
                Format = request.Format,
                UtworzonoUtc = DateTime.UtcNow
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<ZasobUITekstDto>(entity);
        }
    }
}