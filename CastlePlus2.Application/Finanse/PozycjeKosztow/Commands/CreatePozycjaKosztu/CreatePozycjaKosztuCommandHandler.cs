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