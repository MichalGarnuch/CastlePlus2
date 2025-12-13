using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.PozycjeKosztow.Commands.CreatePozycjaKosztu
{
    public class CreatePozycjaKosztuCommandHandler
        : IRequestHandler<CreatePozycjaKosztuCommand, PozycjaKosztuDto>
    {
        private readonly IPozycjaKosztuRepository _repo;
        private readonly IMapper _mapper;

        public CreatePozycjaKosztuCommandHandler(IPozycjaKosztuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PozycjaKosztuDto> Handle(CreatePozycjaKosztuCommand request, CancellationToken ct)
        {
            if (request.IdFaktury <= 0)
                throw new InvalidOperationException("IdFaktury musi być > 0.");

            if (request.IdKategoriiKosztu <= 0)
                throw new InvalidOperationException("IdKategoriiKosztu musi być > 0.");

            if (request.Opis is not null && request.Opis.Length > 200)
                throw new InvalidOperationException("Opis może mieć maksymalnie 200 znaków.");

            if (request.KwotaNetto < 0 || request.KwotaBrutto < 0)
                throw new InvalidOperationException("Kwoty nie mogą być ujemne.");

            var entity = new PozycjaKosztu
            {
                IdFaktury = request.IdFaktury,
                IdKategoriiKosztu = request.IdKategoriiKosztu,
                Opis = request.Opis,
                KwotaNetto = request.KwotaNetto,
                KwotaBrutto = request.KwotaBrutto
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<PozycjaKosztuDto>(entity);
        }
    }
}
