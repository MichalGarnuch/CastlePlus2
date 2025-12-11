using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Lokale.Commands.CreateLokal
{
    /// <summary>
    /// Handler obsługujący utworzenie nowego lokalu.
    /// </summary>
    public class CreateLokalCommandHandler
        : IRequestHandler<CreateLokalCommand, LokalDto>
    {
        private readonly ILokalRepository _lokalRepository;
        private readonly IBudynekRepository _budynekRepository;
        private readonly IMapper _mapper;

        public CreateLokalCommandHandler(
            ILokalRepository lokalRepository,
            IBudynekRepository budynekRepository,
            IMapper mapper)
        {
            _lokalRepository = lokalRepository;
            _budynekRepository = budynekRepository;
            _mapper = mapper;
        }

        public async Task<LokalDto> Handle(
            CreateLokalCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Walidacja istnienia budynku
            var budynek = await _budynekRepository.GetByIdAsync(request.IdBudynku, cancellationToken);
            if (budynek is null)
            {
                throw new InvalidOperationException(
                    $"Budynek o Id = {request.IdBudynku} nie istnieje.");
            }

            // 2. Walidacja unikalności kodu w ramach budynku
            var exists = await _lokalRepository.ExistsWithCodeForBudynekAsync(
                request.IdBudynku,
                request.KodLokalu,
                cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"Dla budynku {request.IdBudynku} istnieje już lokal o kodzie '{request.KodLokalu}'.");
            }

            // 3. Utworzenie encji (klucz generujemy jawnie – Id = IdEncji)
            var entity = new Lokal
            {
                Id = Guid.NewGuid(),
                IdBudynku = request.IdBudynku,
                KodLokalu = request.KodLokalu,
                Powierzchnia = request.Powierzchnia,
                Przeznaczenie = request.Przeznaczenie
            };

            await _lokalRepository.AddAsync(entity, cancellationToken);
            await _lokalRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<LokalDto>(entity);
        }
    }
}
