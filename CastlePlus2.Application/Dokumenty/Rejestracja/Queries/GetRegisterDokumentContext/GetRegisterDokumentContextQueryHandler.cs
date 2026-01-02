using System.Linq;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Dokumenty;
using MediatR;

namespace CastlePlus2.Application.Dokumenty.Rejestracja.Queries.GetRegisterDokumentContext
{
    public class GetRegisterDokumentContextQueryHandler
        : IRequestHandler<GetRegisterDokumentContextQuery, RegisterDokumentContextDto>
    {
        private readonly IEncjaRepository _encjaRepository;

        public GetRegisterDokumentContextQueryHandler(IEncjaRepository encjaRepository)
        {
            _encjaRepository = encjaRepository;
        }

        public async Task<RegisterDokumentContextDto> Handle(GetRegisterDokumentContextQuery request, CancellationToken ct)
        {
            var encje = await _encjaRepository.GetAllAsync(ct);

            var lookup = encje
                .OrderBy(e => e.TypEncji)
                .ThenBy(e => e.KodEncji)
                .Take(500)
                .Select(e =>
                {
                    var label = string.IsNullOrWhiteSpace(e.KodEncji)
                        ? e.TypEncji
                        : $"{e.TypEncji} / {e.KodEncji}";

                    return new EncjaLookupDto
                    {
                        IdEncji = e.Id,
                        TypEncji = e.TypEncji,
                        KodEncji = e.KodEncji,
                        Label = label
                    };
                })
                .ToList();

            return new RegisterDokumentContextDto
            {
                EncjeLookup = lookup
            };
        }
    }
}