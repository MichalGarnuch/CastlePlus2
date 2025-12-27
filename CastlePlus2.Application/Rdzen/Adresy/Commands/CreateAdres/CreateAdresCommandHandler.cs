using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Adresy.Commands.CreateAdres
{
    public class CreateAdresCommandHandler : IRequestHandler<CreateAdresCommand, AdresDto>
    {
        private readonly IAdresRepository _adresRepository;
        private readonly IMapper _mapper;

        public CreateAdresCommandHandler(IAdresRepository adresRepository, IMapper mapper)
        {
            _adresRepository = adresRepository;
            _mapper = mapper;
        }

        public async Task<AdresDto> Handle(CreateAdresCommand request, CancellationToken cancellationToken)
        {
            var entity = new Adres
            {
                Kraj = string.IsNullOrWhiteSpace(request.Kraj) ? "Polska" : request.Kraj,
                Wojewodztwo = request.Wojewodztwo,
                Powiat = request.Powiat,
                Gmina = request.Gmina,
                Miejscowosc = request.Miejscowosc,
                Ulica = request.Ulica,
                Numer = request.Numer,
                KodPocztowy = request.KodPocztowy,
                AdresPelny = request.AdresPelny
            };

            await _adresRepository.AddAsync(entity, cancellationToken);
            await _adresRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AdresDto>(entity);
        }
    }
}