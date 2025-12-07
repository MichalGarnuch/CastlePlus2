using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Adresy.Queries.GetAdresById
{
    public class GetAdresByIdQueryHandler : IRequestHandler<GetAdresByIdQuery, AdresDto?>
    {
        private readonly IAdresRepository _adresRepository;
        private readonly IMapper _mapper;

        public GetAdresByIdQueryHandler(IAdresRepository adresRepository, IMapper mapper)
        {
            _adresRepository = adresRepository;
            _mapper = mapper;
        }

        public async Task<AdresDto?> Handle(GetAdresByIdQuery request, CancellationToken cancellationToken)
        {
            var adres = await _adresRepository.GetByIdAsync(request.IdAdresu, cancellationToken);

            if (adres == null)
                return null;

            return _mapper.Map<AdresDto>(adres);
        }
    }
}

