using AutoMapper;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Slowniki;
using CastlePlus2.Domain.Entities.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.Indeksacje.Commands.CreateIndeksacja
{
    public class CreateIndeksacjaCommandHandler : IRequestHandler<CreateIndeksacjaCommand, IndeksacjaDto>
    {
        private readonly IIndeksacjaRepository _repo;
        private readonly IMapper _mapper;

        public CreateIndeksacjaCommandHandler(IIndeksacjaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IndeksacjaDto> Handle(CreateIndeksacjaCommand request, CancellationToken ct)
        {
            var entity = new Indeksacja
            {
                KodIndeksacji = request.KodIndeksacji,
                Nazwa = request.Nazwa,
                AdresZrodlaURL = request.AdresZrodlaURL
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<IndeksacjaDto>(entity);
        }
    }
}
