using AutoMapper;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Slowniki;
using CastlePlus2.Domain.Entities.Slowniki;
using MediatR;

namespace CastlePlus2.Application.Slowniki.Waluty.Commands.CreateWaluta
{
    public class CreateWalutaCommandHandler : IRequestHandler<CreateWalutaCommand, WalutaDto>
    {
        private readonly IWalutaRepository _repo;
        private readonly IMapper _mapper;

        public CreateWalutaCommandHandler(IWalutaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<WalutaDto> Handle(CreateWalutaCommand request, CancellationToken ct)
        {
            // Waluta ma PK = KodWaluty, więc najpierw sprawdzamy czy nie istnieje.
            var existing = await _repo.GetByKodAsync(request.KodWaluty, ct);
            if (existing != null)
                return _mapper.Map<WalutaDto>(existing);

            var entity = new Waluta
            {
                KodWaluty = request.KodWaluty,
                Nazwa = request.Nazwa
            };

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<WalutaDto>(entity);
        }
    }
}
