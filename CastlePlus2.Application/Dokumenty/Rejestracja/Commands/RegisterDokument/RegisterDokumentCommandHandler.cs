using System;
using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using CastlePlus2.Domain.Entities.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.Rejestracja.Commands.RegisterDokument
{
    public class RegisterDokumentCommandHandler : IRequestHandler<RegisterDokumentCommand, RegisterDokumentResultDto>
    {
        private readonly IEncjaRepository _encjaRepository;
        private readonly IDokumentRepository _dokumentRepository;

        public RegisterDokumentCommandHandler(
            IEncjaRepository encjaRepository,
            IDokumentRepository dokumentRepository)
        {
            _encjaRepository = encjaRepository;
            _dokumentRepository = dokumentRepository;
        }

        public async Task<RegisterDokumentResultDto> Handle(RegisterDokumentCommand request, CancellationToken ct)
        {
            var encja = await _encjaRepository.GetByIdAsync(request.IdEncji, ct);
            if (encja is null)
            {
                throw new InvalidOperationException($"Nie znaleziono encji o Id = {request.IdEncji}.");
            }

            var nazwa = request.Nazwa.Trim();
            var opis = string.IsNullOrWhiteSpace(request.Opis) ? null : request.Opis.Trim();
            var sciezkaPliku = string.IsNullOrWhiteSpace(request.SciezkaPliku)
                ? $"storage/pending/{nazwa}"
                : request.SciezkaPliku.Trim();

            var dokument = new Dokument
            {
                IdEncjiOwner = request.IdEncji,
                Nazwa = nazwa,
                Opis = opis,
                SciezkaPliku = sciezkaPliku,
                DataUtworzenia = DateTime.UtcNow
            };

            var powiazanie = new PowiazanieDokumentu
            {
                IdEncji = request.IdEncji,
                Dokument = dokument
            };

            dokument.Powiazania.Add(powiazanie);

            await _dokumentRepository.AddAsync(dokument, ct);
            await _dokumentRepository.SaveChangesAsync(ct);

            return new RegisterDokumentResultDto
            {
                IdDokumentu = dokument.IdDokumentu
            };
        }
    }
}