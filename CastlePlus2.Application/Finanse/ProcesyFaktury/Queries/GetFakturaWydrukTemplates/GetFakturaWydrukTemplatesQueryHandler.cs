using CastlePlus2.Application.Interfaces.Dokumenty;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.ProcesyFaktury.Queries.GetFakturaWydrukTemplates
{
    public class GetFakturaWydrukTemplatesQueryHandler : IRequestHandler<GetFakturaWydrukTemplatesQuery, List<FakturaWydrukTemplateDto>>
    {
        private const string TemplateTag = "[TEMPLATE:FAKTURA]";
        private const string TemplatePrefix = "templates/finanse/faktury/";

        private readonly IDokumentRepository _dokumentRepository;

        public GetFakturaWydrukTemplatesQueryHandler(IDokumentRepository dokumentRepository)
        {
            _dokumentRepository = dokumentRepository;
        }

        public async Task<List<FakturaWydrukTemplateDto>> Handle(GetFakturaWydrukTemplatesQuery request, CancellationToken ct)
        {
            var dokumenty = await _dokumentRepository.GetAllAsync(cancellationToken: ct);

            return dokumenty
                .Where(d => d.SciezkaPliku.StartsWith(TemplatePrefix, StringComparison.OrdinalIgnoreCase)
                            || (!string.IsNullOrWhiteSpace(d.Opis) && d.Opis.Contains(TemplateTag, StringComparison.OrdinalIgnoreCase)))
                .OrderBy(d => d.Nazwa)
                .Select(d => new FakturaWydrukTemplateDto
                {
                    IdDokumentu = d.IdDokumentu,
                    Nazwa = d.Nazwa,
                    Opis = d.Opis,
                    SciezkaPliku = d.SciezkaPliku,
                    Label = $"{d.Nazwa} ({d.SciezkaPliku})"
                })
                .ToList();
        }
    }
}