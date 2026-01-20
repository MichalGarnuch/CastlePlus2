using AutoMapper;
using CastlePlus2.Application.Interfaces.Konfiguracja;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Konfiguracja;
using CastlePlus2.Domain.Entities.Konfiguracja;
using CastlePlus2.Domain.Entities.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Konfiguracja.ZasobyUI.Commands.CreateZasobUI
{
    public class CreateZasobUICommandHandler : IRequestHandler<CreateZasobUICommand, ZasobUIDto>
    {
        private const string EncjaTyp = "ZASOB_UI";
        private readonly IZasobUIRepository _repo;
        private readonly IEncjaRepository _encjaRepo;
        private readonly IMapper _mapper;

        public CreateZasobUICommandHandler(IZasobUIRepository repo, IEncjaRepository encjaRepo, IMapper mapper)
        {
            _repo = repo;
            _encjaRepo = encjaRepo;
            _mapper = mapper;
        }

        public async Task<ZasobUIDto> Handle(CreateZasobUICommand request, CancellationToken ct)
        {
            var existing = await _repo.GetByKodZasobuAsync(request.KodZasobu, ct);
            if (existing != null)
            {
                return _mapper.Map<ZasobUIDto>(existing);
            }

            var idEncji = Guid.NewGuid();
            var encja = new Encja
            {
                Id = idEncji,
                TypEncji = EncjaTyp,
                KodEncji = string.IsNullOrWhiteSpace(request.KodZasobu)
                    ? null
                    : request.KodZasobu.Length > 40
                        ? request.KodZasobu[..40]
                        : request.KodZasobu
            };

            var entity = new ZasobUI
            {
                IdEncji = idEncji,
                KodZasobu = request.KodZasobu,
                Typ = request.Typ,
                Kategoria = request.Kategoria,
                CzyAktywny = request.CzyAktywny,
                Sort = request.Sort,
                WazneOdUtc = request.WazneOdUtc,
                WazneDoUtc = request.WazneDoUtc,
                UtworzonoUtc = DateTime.UtcNow,
                Encja = encja
            };

            await _encjaRepo.AddAsync(encja, ct);
            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<ZasobUIDto>(entity);
        }
    }
}