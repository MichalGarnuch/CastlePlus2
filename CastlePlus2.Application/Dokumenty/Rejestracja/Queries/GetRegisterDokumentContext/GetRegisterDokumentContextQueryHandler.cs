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
            return new RegisterDokumentContextDto
            {
                EncjeLookup = new List<EncjaLookupDto>()
            };
        }
    }
}