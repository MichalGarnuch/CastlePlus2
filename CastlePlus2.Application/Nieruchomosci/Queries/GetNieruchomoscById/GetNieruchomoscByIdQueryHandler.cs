using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Nieruchomosci.Queries.GetNieruchomoscById
{
    /// <summary>
    /// Handler obsługujący zapytanie GetNieruchomoscByIdQuery.
    /// 
    /// Zadania:
    /// 1. Pobranie encji Nieruchomosc z repozytorium.
    /// 2. Zmapowanie encji na DTO (NieruchomoscDto) przez AutoMapper.
    /// 3. Zwrócenie DTO lub null, jeśli nie znaleziono.
    /// </summary>
    public class GetNieruchomoscByIdQueryHandler
        : IRequestHandler<GetNieruchomoscByIdQuery, NieruchomoscDto?>
    {
        private readonly INieruchomoscRepository _nieruchomoscRepository;
        private readonly IMapper _mapper;

        public GetNieruchomoscByIdQueryHandler(
            INieruchomoscRepository nieruchomoscRepository,
            IMapper mapper)
        {
            _nieruchomoscRepository = nieruchomoscRepository;
            _mapper = mapper;
        }

        public async Task<NieruchomoscDto?> Handle(GetNieruchomoscByIdQuery request, CancellationToken cancellationToken)
        {
            // 1. Pobranie encji z repozytorium.
            var entity = await _nieruchomoscRepository.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null)
            {
                // Brak wyniku – zwracamy null, kontroler API sam zdecyduje czy to 404 czy coś innego.
                return null;
            }

            // 2. Mapowanie encji na DTO.
            var dto = _mapper.Map<NieruchomoscDto>(entity);

            return dto;
        }
    }
}

