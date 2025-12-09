using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Nieruchomosci.Commands.CreateNieruchomosc
{
    /// <summary>
    /// Handler obsługujący komendę CreateNieruchomoscCommand.
    ///
    /// Zadania handlera:
    /// 1. Utworzyć nową encję Nieruchomosc na podstawie danych z komendy.
    /// 2. Wygenerować nowy GUID dla Id (IdEncji).
    /// 3. Zapisać encję w repozytorium (EF Core).
    /// 4. Zwrócić NieruchomoscDto (mapowane przez AutoMappera).
    /// </summary>
    public class CreateNieruchomoscCommandHandler
        : IRequestHandler<CreateNieruchomoscCommand, NieruchomoscDto>
    {
        private readonly INieruchomoscRepository _repository;
        private readonly IMapper _mapper;

        public CreateNieruchomoscCommandHandler(
            INieruchomoscRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<NieruchomoscDto> Handle(
            CreateNieruchomoscCommand request,
            CancellationToken cancellationToken)
        {
            // Tworzymy nową encję domenową
            var entity = new Nieruchomosc
            {
                Id = Guid.NewGuid(),                  // IdEncji (PK)
                Nazwa = request.Nazwa,
                IdAdresuGlownego = request.IdAdresuGlownego,
                IdPodmiotuWlasciciela = request.IdPodmiotuWlasciciela
                // Geometria dojdzie później (np. WKT -> geometry)
            };

            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            // Po zapisie możemy mieć uzupełnione nawigacje (np. AdresGlowny)
            // – AutoMapper użyje profilu NieruchomoscProfile.
            var dto = _mapper.Map<NieruchomoscDto>(entity);

            return dto;
        }
    }
}
