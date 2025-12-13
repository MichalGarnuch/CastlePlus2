using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Domain.Entities.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.PowiazaniaDokumentu.Commands.CreatePowiazanieDokumentu
{
    public class CreatePowiazanieDokumentuCommandHandler
        : IRequestHandler<CreatePowiazanieDokumentuCommand, PowiazanieDokumentuDto>
    {
        private readonly IPowiazanieDokumentuRepository _powiazanieRepo;
        private readonly IDokumentRepository _dokumentRepo;
        private readonly IMapper _mapper;

        public CreatePowiazanieDokumentuCommandHandler(
            IPowiazanieDokumentuRepository powiazanieRepo,
            IDokumentRepository dokumentRepo,
            IMapper mapper)
        {
            _powiazanieRepo = powiazanieRepo;
            _dokumentRepo = dokumentRepo;
            _mapper = mapper;
        }

        public async Task<PowiazanieDokumentuDto> Handle(CreatePowiazanieDokumentuCommand request, CancellationToken ct)
        {
            if (request.IdDokumentu <= 0)
                throw new InvalidOperationException("IdDokumentu musi być > 0.");

            if (request.IdEncji == Guid.Empty)
                throw new InvalidOperationException("IdEncji nie może być pustym GUID.");

            // Czytelny błąd zanim poleci FK z bazy:
            var dokument = await _dokumentRepo.GetByIdAsync(request.IdDokumentu, ct);
            if (dokument is null)
                throw new InvalidOperationException($"Dokument o IdDokumentu={request.IdDokumentu} nie istnieje.");

            var exists = await _powiazanieRepo.ExistsAsync(request.IdDokumentu, request.IdEncji, ct);
            if (exists)
                throw new InvalidOperationException("Takie powiązanie już istnieje (IdDokumentu + IdEncji).");

            var entity = new PowiazanieDokumentu
            {
                IdDokumentu = request.IdDokumentu,
                IdEncji = request.IdEncji
            };

            await _powiazanieRepo.AddAsync(entity, ct);
            await _powiazanieRepo.SaveChangesAsync(ct);

            return _mapper.Map<PowiazanieDokumentuDto>(entity);
        }
    }
}
