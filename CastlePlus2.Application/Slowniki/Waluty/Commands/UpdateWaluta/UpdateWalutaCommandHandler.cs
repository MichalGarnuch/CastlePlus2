using AutoMapper;
using MediatR;
using CastlePlus2.Application.Interfaces.Slowniki;
using CastlePlus2.Contracts.DTOs.Slowniki;

namespace CastlePlus2.Application.Slowniki.Waluty.Commands.UpdateWaluta
{
    public class UpdateWalutaCommandHandler : IRequestHandler<UpdateWalutaCommand, WalutaDto?>
    {
        private readonly IWalutaRepository _repo;
        private readonly IMapper _mapper;

        public UpdateWalutaCommandHandler(IWalutaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<WalutaDto?> Handle(UpdateWalutaCommand request, CancellationToken ct)
        {
            var kod = (request.KodWaluty ?? string.Empty).Trim().ToUpperInvariant();
            var nazwa = (request.Nazwa ?? string.Empty).Trim();

            var entity = await _repo.GetByKodAsync(kod, ct);
            if (entity is null)
                return null;

            entity.Nazwa = nazwa;

            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<WalutaDto>(entity);
        }
    }
}
