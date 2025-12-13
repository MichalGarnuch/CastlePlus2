using System;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using CastlePlus2.Domain.Entities.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.AlokacjeKosztow.Commands.CreateAlokacjaKosztu
{
    public class CreateAlokacjaKosztuCommandHandler
        : IRequestHandler<CreateAlokacjaKosztuCommand, AlokacjaKosztuDto>
    {
        private readonly IAlokacjaKosztuRepository _repo;
        private readonly IMapper _mapper;

        public CreateAlokacjaKosztuCommandHandler(IAlokacjaKosztuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AlokacjaKosztuDto> Handle(CreateAlokacjaKosztuCommand request, CancellationToken ct)
        {
            if (request.IdPozycjiKosztu <= 0)
                throw new InvalidOperationException("IdPozycjiKosztu musi być > 0.");

            if (request.IdEncji == Guid.Empty)
                throw new InvalidOperationException("IdEncji nie może być pustym GUID.");

            if (request.KwotaNetto < 0 || request.KwotaBrutto < 0)
                throw new InvalidOperationException("Kwoty nie mogą być ujemne.");

            var exists = await _repo.ExistsAsync(request.IdPozycjiKosztu, request.IdEncji, ct);
            if (exists)
                throw new InvalidOperationException("Taka alokacja już istnieje (IdPozycjiKosztu + IdEncji).");

            var entity = new AlokacjaKosztu
            {
                IdPozycjiKosztu = request.IdPozycjiKosztu,
                IdEncji = request.IdEncji,
                KwotaNetto = request.KwotaNetto,
                KwotaBrutto = request.KwotaBrutto
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<AlokacjaKosztuDto>(entity);
        }
    }
}
