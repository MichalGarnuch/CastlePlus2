using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using CastlePlus2.Domain.Entities.Media;
using MediatR;

namespace CastlePlus2.Application.Media.RodzajeMediow.Commands.CreateRodzajMedium
{
    public class CreateRodzajMediumCommandHandler : IRequestHandler<CreateRodzajMediumCommand, RodzajMediumDto>
    {
        private readonly IRodzajMediumRepository _repo;
        private readonly IMapper _mapper;

        public CreateRodzajMediumCommandHandler(IRodzajMediumRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RodzajMediumDto> Handle(CreateRodzajMediumCommand request, CancellationToken ct)
        {
            // Walidacje minimalne – bez “magii” w kontrolerze
            request.KodRodzaju = (request.KodRodzaju ?? string.Empty).Trim();
            request.Nazwa = (request.Nazwa ?? string.Empty).Trim();

            if (request.KodRodzaju.Length == 0)
                throw new InvalidOperationException("KodRodzaju jest wymagany.");

            if (request.KodRodzaju.Length > 20)
                throw new InvalidOperationException("KodRodzaju max 20 znaków.");

            if (request.Nazwa.Length == 0)
                throw new InvalidOperationException("Nazwa jest wymagana.");

            if (request.Nazwa.Length > 100)
                throw new InvalidOperationException("Nazwa max 100 znaków.");

            var exists = await _repo.ExistsAsync(request.KodRodzaju, ct);
            if (exists)
                throw new InvalidOperationException("Taki RodzajMedium już istnieje (KodRodzaju).");

            var entity = new RodzajMedium
            {
                KodRodzaju = request.KodRodzaju,
                Nazwa = request.Nazwa
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<RodzajMediumDto>(entity);
        }
    }
}
