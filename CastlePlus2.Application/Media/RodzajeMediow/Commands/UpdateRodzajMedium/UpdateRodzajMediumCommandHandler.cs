using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CastlePlus2.Application.Interfaces.Media;
using CastlePlus2.Contracts.DTOs.Media;
using MediatR;

namespace CastlePlus2.Application.Media.RodzajeMediow.Commands.UpdateRodzajMedium
{
    public sealed class UpdateRodzajMediumCommandHandler : IRequestHandler<UpdateRodzajMediumCommand, RodzajMediumDto?>
    {
        private readonly IRodzajMediumRepository _repo;
        private readonly IMapper _mapper;

        public UpdateRodzajMediumCommandHandler(IRodzajMediumRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RodzajMediumDto?> Handle(UpdateRodzajMediumCommand request, CancellationToken ct)
        {
            var kod = (request.KodRodzaju ?? string.Empty).Trim();
            var nazwa = (request.Nazwa ?? string.Empty).Trim();

            if (kod.Length == 0 || kod.Length > 20) return null;
            if (nazwa.Length == 0 || nazwa.Length > 100) return null;

            var entity = await _repo.GetByIdAsync(kod, ct);
            if (entity is null) return null;

            entity.Nazwa = nazwa;

            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<RodzajMediumDto>(entity);
        }
    }
}
