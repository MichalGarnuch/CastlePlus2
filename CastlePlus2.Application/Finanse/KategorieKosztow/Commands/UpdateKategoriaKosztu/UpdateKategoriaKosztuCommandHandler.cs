using AutoMapper;
using CastlePlus2.Application.Interfaces.Finanse;
using CastlePlus2.Contracts.DTOs.Finanse;
using MediatR;

namespace CastlePlus2.Application.Finanse.KategorieKosztow.Commands.UpdateKategoriaKosztu
{
    public class UpdateKategoriaKosztuCommandHandler : IRequestHandler<UpdateKategoriaKosztuCommand, KategoriaKosztuDto?>
    {
        private readonly IKategoriaKosztuRepository _repo;
        private readonly IMapper _mapper;

        public UpdateKategoriaKosztuCommandHandler(IKategoriaKosztuRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<KategoriaKosztuDto?> Handle(UpdateKategoriaKosztuCommand request, CancellationToken ct)
        {
            var kod = (request.Kod ?? string.Empty).Trim();
            var nazwa = (request.Nazwa ?? string.Empty).Trim();

            var entity = await _repo.GetForUpdateAsync(request.IdKategoriiKosztu, ct);
            if (entity is null)
                return null;

            if (await _repo.ExistsOtherByKodAsync(kod, request.IdKategoriiKosztu, ct))
                throw new InvalidOperationException("Kategoria o takim Kod już istnieje (unikalny indeks).");

            entity.Kod = kod;
            entity.Nazwa = nazwa;

            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<KategoriaKosztuDto>(entity);
        }
    }
}
