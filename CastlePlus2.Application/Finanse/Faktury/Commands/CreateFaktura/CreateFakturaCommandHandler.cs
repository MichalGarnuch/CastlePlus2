using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.Faktury.Commands.CreateFaktura
{
    public class CreateFakturaCommandHandler : IRequestHandler<CreateFakturaCommand, FakturaDto>
    {
        private readonly IFakturaRepository _repo;
        private readonly IMapper _mapper;

        public CreateFakturaCommandHandler(IFakturaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<FakturaDto> Handle(CreateFakturaCommand request, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(request.NumerFaktury))
                throw new InvalidOperationException("NumerFaktury jest wymagany.");

            if (request.NumerFaktury.Length > 60)
                throw new InvalidOperationException("NumerFaktury może mieć maksymalnie 60 znaków.");

            if (request.IdPodmiotu <= 0)
                throw new InvalidOperationException("IdPodmiotu musi być > 0.");

            if (request.KodWaluty is null || request.KodWaluty.Trim().Length != 3)
                throw new InvalidOperationException("KodWaluty musi mieć dokładnie 3 znaki (char(3)).");

            // Unikalny indeks UX_fi_Faktura_Numer
            var exists = await _repo.ExistsByNumerAsync(request.NumerFaktury, ct);
            if (exists)
                throw new InvalidOperationException("Faktura o takim NumerFaktury już istnieje (unikalny indeks).");

            var entity = new Faktura
            {
                NumerFaktury = request.NumerFaktury.Trim(),
                IdPodmiotu = request.IdPodmiotu,
                DataWystawienia = request.DataWystawienia.Date,
                DataSprzedazy = request.DataSprzedazy?.Date,
                KodWaluty = request.KodWaluty.Trim().ToUpperInvariant(),
                KwotaNetto = request.KwotaNetto,
                KwotaBrutto = request.KwotaBrutto
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<FakturaDto>(entity);
        }
    }
}
