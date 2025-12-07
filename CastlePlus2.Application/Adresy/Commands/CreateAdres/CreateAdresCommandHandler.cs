using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Adresy.Commands.CreateAdres
{
    public class CreateAdresCommandHandler : IRequestHandler<CreateAdresCommand, long>
    {
        private readonly IAdresRepository _adresRepository;

        public CreateAdresCommandHandler(IAdresRepository adresRepository)
        {
            _adresRepository = adresRepository;
        }

        public async Task<long> Handle(CreateAdresCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Miejscowosc))
                throw new ArgumentException("Miejscowość jest wymagana.");

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

            return entity.IdAdresu;
        }
    }
}

