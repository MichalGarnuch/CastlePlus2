using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Pomieszczenia.Commands.CreatePomieszczenie
{
    public class CreatePomieszczenieCommandHandler
        : IRequestHandler<CreatePomieszczenieCommand, PomieszczenieDto>
    {
        private readonly IPomieszczenieRepository _pomieszczenieRepository;
        private readonly ILokalRepository _lokalRepository;
        private readonly IMapper _mapper;

        public CreatePomieszczenieCommandHandler(
            IPomieszczenieRepository pomieszczenieRepository,
            ILokalRepository lokalRepository,
            IMapper mapper)
        {
            _pomieszczenieRepository = pomieszczenieRepository;
            _lokalRepository = lokalRepository;
            _mapper = mapper;
        }

        public async Task<PomieszczenieDto> Handle(
            CreatePomieszczenieCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Lokal istnieje?
            var lokal = await _lokalRepository.GetByIdAsync(request.IdEncjiNadrzednej, cancellationToken);
            if (lokal is null)
            {
                throw new InvalidOperationException(
                    $"Lokal o Id = {request.IdEncjiNadrzednej} nie istnieje.");
            }

            // 2. Kod unikalny w ramach lokalu?
            var exists = await _pomieszczenieRepository.ExistsWithCodeForLokalAsync(
                request.IdEncjiNadrzednej,
                request.KodPomieszczenia,
                cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"Dla lokalu {request.IdEncjiNadrzednej} istnieje już pomieszczenie o kodzie '{request.KodPomieszczenia}'.");
            }

            // 3. Tworzymy encję – UWAGA: Id, nie IdEncji
            var entity = new Pomieszczenie
            {
                Id = Guid.NewGuid(), // PK -> kolumna [IdEncji] przez EncjaConfiguration
                IdEncjiNadrzednej = request.IdEncjiNadrzednej,
                KodPomieszczenia = request.KodPomieszczenia,
                Powierzchnia = request.Powierzchnia
            };

            await _pomieszczenieRepository.AddAsync(entity, cancellationToken);
            await _pomieszczenieRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<PomieszczenieDto>(entity);
        }
    }
}
