using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Rdzen;
using CastlePlus2.Contracts.DTOs.Rdzen;
using CastlePlus2.Domain.Entities.Rdzen;
using MediatR;

namespace CastlePlus2.Application.Rdzen.Budynki.Commands.CreateBudynek
{
    /// <summary>
    /// Handler tworzący nowy budynek.
    /// </summary>
    public class CreateBudynekCommandHandler : IRequestHandler<CreateBudynekCommand, BudynekDto>
    {
        private readonly IBudynekRepository _repository;
        private readonly IMapper _mapper;

        public CreateBudynekCommandHandler(IBudynekRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BudynekDto> Handle(CreateBudynekCommand request, CancellationToken cancellationToken)
        {
            // Tworzymy encję zgodnie z rdzeniem (Encja + Budynek)
            var entity = new Budynek
            {
                // Id z Encja – generujemy tutaj.
                Id = Guid.NewGuid(),
                IdNieruchomosci = request.IdNieruchomosci,
                KodBudynku = request.KodBudynku,
                IdAdresu = request.IdAdresu,
                Kondygnacje = request.Kondygnacje,
                PowierzchniaUzytkowa = request.PowierzchniaUzytkowa
            };

            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            // Mapowanie encji do DTO za pomocą AutoMappera
            return _mapper.Map<BudynekDto>(entity);
        }
    }
}
