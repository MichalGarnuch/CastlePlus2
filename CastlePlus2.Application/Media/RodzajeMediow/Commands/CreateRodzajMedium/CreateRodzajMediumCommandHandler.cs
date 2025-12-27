using System;
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