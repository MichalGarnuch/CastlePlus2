using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.PozycjeKosztow.Commands.UpdatePozycjaKosztu
{
    public class UpdatePozycjaKosztuCommandHandler : IRequestHandler<UpdatePozycjaKosztuCommand, PozycjaKosztuDto?>
    {
        private readonly IPozycjaKosztuRepository _repo;
        private readonly IMapper _mapper;

        public UpdatePozycjaKosztuCommandHandler(IPozycjaKosztuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PozycjaKosztuDto?> Handle(UpdatePozycjaKosztuCommand request, CancellationToken ct)
        {
            if (request.IdFaktury <= 0)
                throw new InvalidOperationException("IdFaktury musi by > 0.");

            if (request.IdKategoriiKosztu <= 0)
                throw new InvalidOperationException("IdKategoriiKosztu musi by > 0.");

            var entity = await _repo.GetForUpdateAsync(request.IdPozycjiKosztu, ct);
            if (entity is null)
                return null;

            entity.IdFaktury = request.IdFaktury;
            entity.IdKategoriiKosztu = request.IdKategoriiKosztu;
            entity.Opis = string.IsNullOrWhiteSpace(request.Opis) ? null : request.Opis.Trim();
            entity.KwotaNetto = request.KwotaNetto;
            entity.KwotaBrutto = request.KwotaBrutto;

            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<PozycjaKosztuDto>(entity);
        }
    }
}
